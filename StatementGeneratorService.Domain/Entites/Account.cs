using StatementGeneratorService.Domain.Common;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Domain.Entites
{
    public class Account : BaseEntity
    {
        public int CustomerId { get; set; }

        public string AccountNumber { get; set; } = default!;

        public AccountType AccountType { get; set; }

        public decimal Balance { get; set; }

        public AccountStatus Status { get; set; }

        DateTime? EmailSentAt { get; set; }

        // Navigations
        public Customer Customer { get; set; } = default!;
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

        public ICollection<Statement> Statements { get; set; } = new List<Statement>();

    }
}