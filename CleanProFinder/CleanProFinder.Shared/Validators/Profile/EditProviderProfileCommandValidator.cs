using CleanProFinder.Shared.Dto.Profile;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanProFinder.Shared.Validators.Profile
{
    public class EditProviderProfileCommandValidator : AbstractValidator<ProviderProfileDto>
    {
        public EditProviderProfileCommandValidator()
        {
            RuleFor(p => p.Name)
                .NotNull();

            RuleFor(p => p.PhoneNumber)
                .Matches(new Regex(@"\+\d{12}$"));

            RuleFor(p => p.Site)
                .NotNull();

        }
    }
}