using Microsoft.Extensions.Logging;
using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views;

namespace CleanProFinder.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
		builder.Logging.AddDebug();
#endif
            builder.Services.AddTransient<RolePage>();
            builder.Services.AddTransient<RoleViewModel>();

            return builder.Build();
        }
    }
}