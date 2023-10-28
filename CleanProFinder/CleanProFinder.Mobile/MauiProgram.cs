using Microsoft.Extensions.Logging;
using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views;
using CommunityToolkit.Maui;

namespace CleanProFinder.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<CustomerStartingPage>();
            builder.Services.AddTransient<CustomerStartingViewModel>();
            builder.Services.AddTransient<ServiceProviderStartingPage>();
            builder.Services.AddTransient<ServiceProviderStartingViewModel>();

            return builder.Build();
        }
    }
}