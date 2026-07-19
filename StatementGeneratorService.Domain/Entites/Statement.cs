using StatementGeneratorService.Domain.Common;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Domain.Entites
{
    public class Statement : BaseEntity
    {
        public int AccountId { get; set; }


        public int Month { get; set; }

        public int Year { get; set; }


        public decimal OpeningBalance { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal ClosingBalance { get; set; }


        public StatementStatus Status { get; set; }


        public DateTime GeneratedAt { get; set; }


        // Navigation

        public Account Account { get; set; } = default!;
        public ICollection<StatementTransaction> Transactions { get; set; } = new List<StatementTransaction>();

    }
}
