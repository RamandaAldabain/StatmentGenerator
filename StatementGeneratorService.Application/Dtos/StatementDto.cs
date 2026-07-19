namespace StatementGeneratorService.Application.Dtos
{
    public class StatementDto : BaseDto
    {
        public int CustomerId { get; set; }


        public string CustomerName { get; set; } = default!;

        public string Email { get; set; } = default!;


        public int Month { get; set; }

        public int Year { get; set; }


        public decimal OpeningBalance { get; set; }

        public decimal TotalCredits { get; set; }

        public decimal TotalDebits { get; set; }

        public decimal ClosingBalance { get; set; }


        public List<TransactionDto> Transactions { get; set; }= new();
    }
}
