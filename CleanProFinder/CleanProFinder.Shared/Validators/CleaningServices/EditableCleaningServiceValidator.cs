using CleanProFinder.Shared.Dto.CleaningServices;
using FluentValidation;

namespace CleanProFinder.Shared.Validators.CleaningServices
{
    public class EditableCleaningServiceValidator<TService> : AbstractValidator<TService> where TService : EditableCleaningServiceDto
    {
        public EditableCleaningServiceValidator()
        {
            RuleFor(s => s.Name).NotEmpty();
            RuleFor(s => s.Description).NotEmpty();
            RuleFor(s => s.Price).GreaterThan(0);
        }
    }
}