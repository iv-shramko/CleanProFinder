using CleanProFinder.Server.Hubs.Notifiers;
using CleanProFinder.Server.Mediator.Pipeline;
using CleanProFinder.Server.Services.Implementations;
using CleanProFinder.Server.Services.Interfaces;
using MediatR;

namespace CleanProFinder.Server.BuildExtensions
{
    internal static class ServicesInjection
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
            services.AddTransient<IValidationService, ValidationService>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddTransient<RequestNotifier>();
        }
    }
}
