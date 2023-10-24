using CleanProFinder.Server.Features.Account;
using CleanProFinder.Shared.Validators.Account;
using FluentValidation;

namespace CleanProFinder.Server.BuildExtensions
{
    internal static class ValidatorsInjection
    {
        internal static void AddValidators(this IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateServiceUserCommand>, CreateServiceUserCommandValidator>();
        }
    }
}
