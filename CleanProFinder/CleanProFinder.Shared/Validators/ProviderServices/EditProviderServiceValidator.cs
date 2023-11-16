using CleanProFinder.Shared.Dto.CleaningServices;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Validators.ProviderServices
{
    public class EditProviderServiceValidator : AbstractValidator<EditProviderServiceDto>
    {
        public EditProviderServiceValidator()
        {
            RuleFor(pS => pS.Description)
                .NotNull()
                .NotEmpty();

            RuleFor(pS => pS.Price)
                .GreaterThan(0);
        }
    }
}