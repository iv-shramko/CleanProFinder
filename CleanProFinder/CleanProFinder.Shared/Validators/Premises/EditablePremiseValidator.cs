using CleanProFinder.Shared.Dto.Premises;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.Premises
{
    public class EditablePremiseValidator<TPremise> : AbstractValidator<TPremise> where TPremise : EditablePremiseDto
    {
        public EditablePremiseValidator()
        {
            RuleFor(p => p.Square).GreaterThan(0);
            RuleFor(p => p.Address).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
        }
    }
}
