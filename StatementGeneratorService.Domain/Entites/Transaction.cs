using StatementGeneratorService.Domain.Common;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Domain.Entites
{
    public class Transaction : BaseEntity
    {
        public int AccountId { get; set; }

        public TransactionType Type { get; set; }

        public decimal Amount { get; set; }


        public string Description { get; set; } = default!;

        public string ReferenceNumber { get; set; } = default!;

        public DateTime TransactionDate { get; set; }


        // Navigation
        public Account Account { get; set; } = default!;
    }
}
