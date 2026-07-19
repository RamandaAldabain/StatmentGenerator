using StatementGeneratorService.Domain.Common;
using StatementGeneratorService.Domain.Enum;

namespace StatementGeneratorService.Domain.Entites
{
    public class Customer : BaseEntity
    {
        public string FirstName { get; set; } = default!;

        public string LastName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;

        public string Address { get; set; } = default!;

        public CustomerStatus Status { get; set; }

        // Navigations 
        public ICollection<Account> Accounts { get; set; } = new List<Account>();


    }

}
