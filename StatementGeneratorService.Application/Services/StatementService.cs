using StatementGeneratorService.Application.Dtos;
using StatementGeneratorService.Application.Interfaces;
using StatementGeneratorService.Application.IRepositories;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Domain.Enum;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace StatementGeneratorService.Application.Services
{
    public class StatementService : IStatementService
    {
        private readonly ICustomerRepository _customerRepository;

        private readonly ITransactionRepository _transactionRepository;

        private readonly IStatementRepository _statementRepository;

        private readonly IEmailService _emailService;
        private readonly ILogger<StatementService>? _logger;



        public StatementService(ICustomerRepository customerRepository,ITransactionRepository transactionRepository,IStatementRepository statementRepository, IEmailService emailService, ILogger<StatementService>? logger = null)
        {
            _customerRepository = customerRepository;

            _transactionRepository = transactionRepository;

            _statementRepository = statementRepository;

            _emailService = emailService;

            _logger = logger;

        }



        public async Task<StatementDto?>
            GetStatementAsync(int customerId,int month, int year)
        {

            var statement =await _statementRepository.GetByCustomerAndMonthAsync(customerId,month,year);
            if (statement == null)
                return null;

            return MapToDto(statement);
        }





        public async Task<StatementDto> GenerateStatementAsync(int customerId, int month, int year)
        {
            _logger?.LogInformation("GenerateStatementAsync called for CustomerId={CustomerId} Month={Month} Year={Year}", customerId, month, year);

            // if a statement already exists for this customer/month/year return it
            var existing = await GetStatementAsync(customerId, month, year);

            var customer = await _customerRepository.GetByIdWithAccountAsync(customerId);

            if (customer == null)
            {
                _logger?.LogWarning("Customer not found: {CustomerId}", customerId);
                throw new Exception("Customer not found");
            }

            var accounts = customer.Accounts.ToList();
            if (!accounts.Any())
            {
                _logger?.LogWarning("Customer {CustomerId} has no accounts", customerId);
                throw new Exception("Customer has no accounts");
            }

            StatementDto? firstResult = null;

            // generate/send statement for each account
            foreach (var acct in accounts)
            {
                var existingForAccount = await _statementRepository.GetByAccountAndMonthAsync(acct.Id, month, year);
                if (existingForAccount != null)
                {
                    _logger?.LogInformation("Statement already exists for AccountId={AccountId} Month={Month} Year={Year}. Sending existing.", acct.Id, month, year);
                    var dto = MapToDto(existingForAccount);
                    await SendStatementEmail(customer, acct, dto);
                    if (firstResult == null) firstResult = dto;
                    continue;
                }

                var transactions = await _transactionRepository.GetTransactionsByAccountAndMonthAsync(acct.Id, month, year);

                var credits = transactions.Where(x => x.Type == TransactionType.Deposit).Sum(x => x.Amount);
                var debits = transactions.Where(x => x.Type != TransactionType.Deposit).Sum(x => x.Amount);

                var stmt = new Statement
                {
                    AccountId = acct.Id,
                    Month = month,
                    Year = year,
                    TotalCredits = credits,
                    TotalDebits = debits,
                    OpeningBalance = acct.Balance - credits + debits,
                    ClosingBalance = acct.Balance,
                    GeneratedAt = DateTime.UtcNow
                };

                await _statementRepository.AddAsync(stmt);
                await _statementRepository.SaveChangesAsync();

                var createdDto = MapToDto(stmt);
                await SendStatementEmail(customer, acct, createdDto);

                if (firstResult == null) firstResult = createdDto;
            }

            return firstResult!;
        }
        private async Task SendStatementEmail(Customer customer, Account account, StatementDto statement)
        {
            try
            {
                var subject = $"Account Statement - {statement.Month:D2}/{statement.Year:D4}";

                var body = $@"<!doctype html>
                        <html>
                        <head>
                            <meta charset='utf-8' />
                            <title>Account Statement</title>
                            <style>
                                body {{
                                    font-family: Arial, Helvetica, sans-serif;
                                    color: #333;
                                }}

                                .container {{
                                    max-width: 700px;
                                    margin: 0 auto;
                                    padding: 16px;
                                }}

                                .header {{
                                    background: #f5f5f5;
                                    padding: 12px;
                                    border-radius: 4px;
                                }}

                                table {{
                                    width: 100%;
                                    border-collapse: collapse;
                                    margin-top: 12px;
                                }}

                                th, td {{
                                    text-align: left;
                                    padding: 8px;
                                    border-bottom: 1px solid #eaeaea;
                                }}
                            </style>
                        </head>
                        <body>

                        <div class='container'>

                        <div class='header'>
                        <h2>Account Statement</h2>
                        <p>
                        Account Type: {account.AccountType}
                        &nbsp;|&nbsp;
                        Period: {statement.Month:D2}/{statement.Year}
                        </p>
                        </div>

                        <p>
                        Dear {customer.FirstName} {customer.LastName},
                        </p>

                        <p>
                        Below is a summary of your account activity for the requested period.
                        </p>

                        <table>
                        <tr>
                        <th>Description</th>
                        <th>Amount</th>
                        </tr>

                        <tr>
                        <td>Opening Balance</td>
                        <td>{statement.OpeningBalance:C}</td>
                        </tr>

                        <tr>
                        <td>Total Credits</td>
                        <td>{statement.TotalCredits:C}</td>
                        </tr>

                        <tr>
                        <td>Total Debits</td>
                        <td>{statement.TotalDebits:C}</td>
                        </tr>

                        <tr>
                        <td><strong>Closing Balance</strong></td>
                        <td><strong>{statement.ClosingBalance:C}</strong></td>
                        </tr>
                        </table>

                        <p>
                        If you have any questions regarding this statement, please contact customer support.
                        </p>

                        <p>
                        Regards,<br/>
                        Statement Generator Service
                        </p>

                        </div>

                        </body>
                        </html>";

                await _emailService.SendStatementAsync(
                    customer.Email,
                    subject,
                    body);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send statement email to {Email} for StatementId={StatementId}", customer.Email, statement.Id);
            }
        }



        private StatementDto MapToDto( Statement statement)
        {
            return new StatementDto
            {
                Id = statement.Id,

                Month = statement.Month,

                Year = statement.Year,

                OpeningBalance = statement.OpeningBalance,

                TotalCredits =   statement.TotalCredits,

                TotalDebits = statement.TotalDebits,

                ClosingBalance = statement.ClosingBalance   
                   
            };
        }
    }
    }
