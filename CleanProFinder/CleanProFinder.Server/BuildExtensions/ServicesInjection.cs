using CleanProFinder.Server.Services.Implementations;
using CleanProFinder.Server.Services.Interfaces;

namespace CleanProFinder.Server.BuildExtensions
{
    internal static class ServicesInjection
    {
        internal static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<ITokenGenerator, JwtTokenGenerator>();
        }
    }
}
