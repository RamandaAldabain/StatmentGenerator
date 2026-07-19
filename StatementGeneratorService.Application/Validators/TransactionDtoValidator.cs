using FluentValidation;
using StatementGeneratorService.Application.Dtos;

namespace StatementGeneratorService.Application.Validators
{
    public class TransactionDtoValidator : AbstractValidator<TransactionDto>
    {
        public TransactionDtoValidator()
        {
            RuleFor(x => x.AccountId).GreaterThan(0);
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Transaction date cannot be in the future");
            RuleFor(x => x.Description).MaximumLength(500);
        }
    }
}
