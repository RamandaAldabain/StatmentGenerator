using StatementGeneratorService.Domain.Common;

namespace StatementGeneratorService.Domain.Entites
{
    public class StatementTransaction : BaseEntity
    {
        public int StatementId { get; set; }


        public DateTime TransactionDate { get; set; }


        public string Description { get; set; } = default!;


        public decimal Debit { get; set; }


        public decimal Credit { get; set; }


        public decimal BalanceAfterTransaction { get; set; }


        // Navigation

        public Statement Statement { get; set; } = default!;
    }
}
