using CleanProFinder.Shared.Dto.Profile;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanProFinder.Shared.Validators.Profile
{
    public class EditUserProfileCommandValidator : AbstractValidator<UserProfileDto>
    {
        public EditUserProfileCommandValidator()
        {
            RuleFor(p => p.FirstName)
                .NotNull();

            RuleFor(p => p.LastName)
                .NotNull();

            RuleFor(p => p.PhoneNumber)
                .NotNull()
                .Matches(new Regex(@"\+\d{12}$"));
        }
    }
}