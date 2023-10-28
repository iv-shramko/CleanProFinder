
using CleanProFinder.Shared.Dto.Profile;
using FluentValidation;
using System.Text.RegularExpressions;

namespace CleanProFinder.Shared.Validators.Profile
{
    public class EditUserProfileCommandValidator : AbstractValidator<UserProfileDto>
    {
        public EditUserProfileCommandValidator()
        {
            RuleFor(p => p.PhoneNumber)
                .Matches(new Regex(@"\+\d{13}$"));
        }
    }
}