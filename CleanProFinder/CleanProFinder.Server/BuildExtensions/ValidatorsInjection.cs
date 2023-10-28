using CleanProFinder.Server.Features.Account;
using CleanProFinder.Server.Features.Profile;
using CleanProFinder.Shared.Validators.Account;
using CleanProFinder.Shared.Validators.Profile;
using FluentValidation;

namespace CleanProFinder.Server.BuildExtensions
{
    internal static class ValidatorsInjection
    {
        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateServiceUserCommand>, CreateServiceUserCommandValidator>();
            services.AddTransient<IValidator<EditUserProfileCommand>, EditUserProfileCommandValidator>();
            services.AddTransient<IValidator<EditProviderProfileCommand>, EditProviderProfileCommandValidator>();
        }
    }
}
