namespace StatementGeneratorService.Application.Interfaces
{
    public interface IEmailService
    {
        Task SendStatementAsync(string toEmail,string subject,string body);
    }
}
