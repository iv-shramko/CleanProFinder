using System.Reflection;
using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Services;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;

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

            var stream = Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream("CleanProFinder.Mobile.Properties.appsettings.json");
            
            if (stream != null)
            {
                var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();

                builder.Configuration.AddConfiguration(config);
            }

#if DEBUG
            builder.Logging.AddDebug();
#endif
            
            builder.Services.AddSingleton<AppShellViewModel>();

            builder.Services.AddTransient<RolePage>();
            builder.Services.AddTransient<RoleViewModel>();

            builder.Services.AddTransient<RegistrationPage>();
            builder.Services.AddTransient<RegistrationViewModel>();

            builder.Services.AddTransient<LoginPage>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddTransient<ServiceUserStartingPage>();
            builder.Services.AddTransient<ServiceUserStartingViewModel>();

            builder.Services.AddTransient<ServiceProviderStartingPage>();
            builder.Services.AddTransient<ServiceProviderStartingViewModel>();

            builder.Services.AddTransient<InitialEditProfilePage>();
            builder.Services.AddTransient<InitialEditProfileViewModel>();

            builder.Services.AddTransient<EditProfilePage>();
            builder.Services.AddTransient<EditProfileViewModel>();
          
            builder.Services.AddTransient<ServiceUserPremisesPage>();
            builder.Services.AddTransient<ServiceUserPremisesViewModel>();

            builder.Services.AddTransient<ServiceUserAddPremisePage>();
            builder.Services.AddTransient<ServiceUserAddPremiseViewModel>();

            builder.Services.AddTransient<ServiceUserEditPremisePage>();
            builder.Services.AddTransient<ServiceUserEditPremiseViewModel>();

            builder.Services.AddSingleton<IHttpService, HttpService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<IUserProfileService, UserProfileService>();
            builder.Services.AddSingleton<IProviderService, ProviderService>();
            builder.Services.AddSingleton<IUserPremiseService, UserPremiseService>();

            return builder.Build();
        }
    }
}