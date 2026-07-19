namespace StatementGeneratorService.Application.Dtos
{
    public class AccountDto : BaseDto
    {
        public int CustomerId { get; set; }

        public string AccountNumber { get; set; } = default!;

        public string AccountType { get; set; } = default!;

        public decimal Balance { get; set; }

        public string Status { get; set; } = default!;
    }
}
