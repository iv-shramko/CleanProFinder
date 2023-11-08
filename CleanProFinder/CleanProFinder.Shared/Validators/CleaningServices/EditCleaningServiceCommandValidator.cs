using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Validators.Premises;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.CleaningServices
{
    public class EditCleaningServiceCommandValidator : EditableCleaningServiceValidator<EditCleaningServiceCommandDto>
    {
        public EditCleaningServiceCommandValidator()
        {
            RuleFor(s => s.Id).NotEmpty();
        }
    }
}