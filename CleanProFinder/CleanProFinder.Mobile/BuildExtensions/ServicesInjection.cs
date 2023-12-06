using CleanProFinder.Mobile.Services.Implementations;
using CleanProFinder.Mobile.Services.Interfaces;

namespace CleanProFinder.Mobile.BuildExtensions;

internal static class ServicesInjection
{
    internal static void AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IHttpService, HttpService>();
        services.AddSingleton<IAuthService, AuthService>();
        services.AddSingleton<IDialogService, DialogService>();
        services.AddSingleton<IUserProfileService, UserProfileService>();
        services.AddSingleton<IProviderService, ProviderService>();
        services.AddSingleton<IPremiseService, PremiseService>();
        services.AddSingleton<ICleaningService, CleaningService>();
        services.AddSingleton<IRequestService, RequestService>();
        services.AddSingleton<INotificationService, NotificationService>();
        services.AddSingleton<ISavedProviderService, SavedProviderService>();
    }
}