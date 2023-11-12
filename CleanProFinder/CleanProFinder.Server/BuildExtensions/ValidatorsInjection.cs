using CleanProFinder.Server.Features.Account;
using CleanProFinder.Server.Features.CleaningServices;
using CleanProFinder.Server.Features.Premises;
using CleanProFinder.Server.Features.Profile;
using CleanProFinder.Server.Features.Requests;
using CleanProFinder.Shared.Validators.Account;
using CleanProFinder.Shared.Validators.CleaningServices;
using CleanProFinder.Shared.Validators.Premises;
using CleanProFinder.Shared.Validators.Profile;
using CleanProFinder.Shared.Validators.Request;
using FluentValidation;

namespace CleanProFinder.Server.BuildExtensions
{
    internal static class ValidatorsInjection
    {
        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateServiceUserCommand>, CreateServiceUserCommandValidator>();
            services.AddTransient<IValidator<CreateServiceProviderCommand>, CreateServiceProviderCommandValidator>();
            services.AddTransient<IValidator<CreateIdentityUserCommand>, CreateIdentityUserCommandValidator>();
            services.AddTransient<IValidator<EditUserProfileCommand>, EditUserProfileCommandValidator>();
            services.AddTransient<IValidator<EditProviderProfileCommand>, EditProviderProfileCommandValidator>();
            services.AddTransient<IValidator<CreatePremiseCommand>, EditablePremiseValidator<CreatePremiseCommand>>();
            services.AddTransient<IValidator<CreateCleaningServiceCommand>, EditableCleaningServiceValidator<CreateCleaningServiceCommand>>();
            services.AddTransient<IValidator<EditPremiseCommand>, EditPremiseCommandValidator>();
            services.AddTransient<IValidator<EditCleaningServiceCommand>, EditCleaningServiceCommandValidator>();
            services.AddTransient<IValidator<CreateRequestCommand>, CreateRequestCommandValidator>();
        }
    }
}
