namespace StatementGeneratorService.Application.Dtos
{
    public class CustomerDto : BaseDto
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Address { get; set; } = default!;
    }
}
