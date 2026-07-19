namespace StatementGeneratorService.Application.Dtos
{
    public class TransactionDto : BaseDto
    {
        public int AccountId { get; set; }
        public DateTime Date { get; set; }

        public string Description { get; set; } = default!;

        public decimal Amount { get; set; }
        public string ReferenceNumber { get; set; } = default!;
        public string Type { get; set; } = default!;
    }
}
