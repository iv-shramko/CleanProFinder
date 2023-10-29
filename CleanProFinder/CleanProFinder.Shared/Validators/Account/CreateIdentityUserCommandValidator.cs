using CleanProFinder.Shared.Dto.Account;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Validators.Account
{
    public class CreateIdentityUserCommandValidator : AbstractValidator<CredentialsDto>
    {
        public CreateIdentityUserCommandValidator()
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