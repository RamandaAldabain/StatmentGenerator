using FluentValidation;
using StatementGeneratorService.Application.Dtos;

namespace StatementGeneratorService.Application.Validators
{
    public class AccountDtoValidator : AbstractValidator<AccountDto>
    {
        public AccountDtoValidator()
        {
            RuleFor(x => x.CustomerId).GreaterThan(0);
            RuleFor(x => x.AccountNumber).NotEmpty();
            RuleFor(x => x.Balance).GreaterThanOrEqualTo(0);
        }
    }
}
