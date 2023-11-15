using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Profile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CleanProFinder.Shared.Validators.ProviderServices
{
    public class EditProviderServicesValidator : AbstractValidator<EditProviderServicesDto>
    {
        public EditProviderServicesValidator()
        {
            RuleForEach(x => x.Services)
                .SetValidator(new EditProviderServiceValidator());
        }
    }
}