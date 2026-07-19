using Microsoft.EntityFrameworkCore;
using StatementGeneratorService.Domain.Entites;
using StatementGeneratorService.Domain.Enum;



namespace StatementGeneratorService.Infrastructure.Database
{

    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Customer> Customers { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Statement> Statements { get; set; }

        public DbSet<StatementTransaction> StatementTransactions { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly);

            // Seed sample customers, accounts and transactions for development/testing
            var customers = new[]
            {
                new Customer
                {
                    Id = 1,
                    FirstName = "John",
                    LastName = "Doe",
                    Email = "john.doe@example.com",
                    PhoneNumber = "555-0100",
                    Address = "123 Main St",
                    Status = CustomerStatus.Active,
                    CreatedAt = new DateTime(2026, 7, 1)
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Jane",
                    LastName = "Smith",
                    Email = "jane.smith@example.com",
                    PhoneNumber = "555-0200",
                    Address = "456 Oak Ave",
                    Status = CustomerStatus.Active,
                    CreatedAt = new DateTime(2026, 7, 1)
                }
            };

            var accounts = new[]
            {
                new Account
                {
                    Id = 1,
                    CustomerId = 1,
                    AccountNumber = "ACC1000001",
                    AccountType = AccountType.Salary,
                    Balance = 1500.00m,
                    Status = AccountStatus.Active,
                    CreatedAt = new DateTime(2026, 7, 1)
                },
                new Account
                {
                    Id = 2,
                    CustomerId = 1,
                    AccountNumber = "ACC1000002",
                    AccountType = AccountType.Savings,
                    Balance = 2500.00m,
                    Status = AccountStatus.Active,
                    CreatedAt = new DateTime(2026, 7, 1)
                },
                new Account
                {
                    Id = 3,
                    CustomerId = 2,
                    AccountNumber = "ACC2000001",
                    AccountType = AccountType.Salary,
                    Balance = 500.00m,
                    Status = AccountStatus.Active,
                    CreatedAt = new DateTime(2026, 7, 1)
                }
            };

            var transactions = new[]
            {
                new Transaction
                {
                    Id = 1,
                    AccountId = 1,
                    Type = TransactionType.Deposit,
                    Amount = 500.00m,
                    Description = "Initial deposit",
                    ReferenceNumber = "REF-1001",
                    TransactionDate = new DateTime(2026, 6, 1),
                    CreatedAt = new DateTime(2026, 6, 1)
                },
                new Transaction
                {
                    Id = 2,
                    AccountId = 1,
                    Type = TransactionType.Withdrawal,
                    Amount = 100.00m,
                    Description = "ATM withdrawal",
                    ReferenceNumber = "REF-1002",
                    TransactionDate = new DateTime(2026, 6, 15),
                    CreatedAt = new DateTime(2026, 6, 15)
                },
                new Transaction
                {
                    Id = 3,
                    AccountId = 2,
                    Type = TransactionType.Deposit,
                    Amount = 2000.00m,
                    Description = "Paycheck",
                    ReferenceNumber = "REF-2001",
                    TransactionDate = new DateTime(2026, 5, 30),
                    CreatedAt = new DateTime(2026, 5, 30)
                },
                new Transaction
                {
                    Id = 4,
                    AccountId = 3,
                    Type = TransactionType.Deposit,
                    Amount = 500.00m,
                    Description = "Gift",
                    ReferenceNumber = "REF-3001",
                    TransactionDate = new DateTime(2026, 4, 20),
                    CreatedAt = new DateTime(2026, 4, 20)
                }
            };

            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<Account>().HasData(accounts);
            modelBuilder.Entity<Transaction>().HasData(transactions);
        }
    }
}