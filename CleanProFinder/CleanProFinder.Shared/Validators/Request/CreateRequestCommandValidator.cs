using CleanProFinder.Shared.Dto.Request;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.Request
{
    public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommandDto>
    {
        public CreateRequestCommandValidator()
        {
            RuleFor(c => c.PremiseId).NotEmpty();
        }
    }
}
