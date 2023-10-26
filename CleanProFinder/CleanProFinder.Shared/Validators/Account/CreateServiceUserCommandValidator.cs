using CleanProFinder.Shared.Dto.Account;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.Account
{
    public class CreateServiceUserCommandValidator : AbstractValidator<CreateServiceUserCommandDto>
    {
        public CreateServiceUserCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Password)
                .NotEmpty()
                .MinimumLength(8);
        }
    }
}
