using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Domain.Entites;

namespace StatementGeneratorService.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> options, ILogger<EmailService> logger)
        {
            _settings = options.Value;
            _logger = logger;
        }

        public Task SendStatementAsync(string toEmail, string subject, string body)
        {
            var message = new MimeMessage();
            message.From.Add(MailboxAddress.Parse(_settings.Email));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject ?? string.Empty;

            var builder = new BodyBuilder { HtmlBody = body ?? string.Empty };
            message.Body = builder.ToMessageBody();

            _logger.LogInformation("Sending statement email to {ToEmail}", toEmail);
            return SendAsync(message);
        }

        private async Task SendAsync(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                SecureSocketOptions socketOptions = _settings.EnableSsl
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.StartTlsWhenAvailable;
                _logger.LogDebug("Connecting to SMTP {Host}:{Port} (SSL:{EnableSsl})", _settings.Host, _settings.Port, _settings.EnableSsl);
                await client.ConnectAsync(_settings.Host, _settings.Port, socketOptions);

                if (!string.IsNullOrWhiteSpace(_settings.Email) && !string.IsNullOrWhiteSpace(_settings.Password))
                {
                    await client.AuthenticateAsync(_settings.Email, _settings.Password);
                }

                await client.SendAsync(message);
                _logger.LogInformation("Email sent to {To}", string.Join(',', message.To.Select(x => x.ToString())));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send email to {To}", message.To);
            }
            finally
            {
                try { await client.DisconnectAsync(true); } catch (Exception ex) { _logger.LogDebug(ex, "Error while disconnecting SMTP client"); }
            }
        }
    }
}
