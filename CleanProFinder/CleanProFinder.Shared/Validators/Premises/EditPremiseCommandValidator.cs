using CleanProFinder.Shared.Dto.Premises;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.Premises
{
    public class EditPremiseCommandValidator : EditablePremiseValidator<EditPremiseCommandDto>
    {
        public EditPremiseCommandValidator()
        {
            RuleFor(c => c.Id).NotEmpty();
        }
    }
}
