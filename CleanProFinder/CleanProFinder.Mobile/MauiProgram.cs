using CleanProFinder.Mobile.Services.Implementations;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.ViewModels.Authentication;
using CleanProFinder.Mobile.ViewModels.ServiceProvider;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.Profile;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;
using CleanProFinder.Mobile.ViewModels.ServiceUser;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Profile;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.Authentication;
using CleanProFinder.Mobile.Views.ServiceProvider;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Mobile.Views.ServiceUser;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
                    fonts.AddFont("Chillax-Semibold.ttf", "Chillax");
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

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });

            Microsoft.Maui.Handlers.EditorHandler.Mapper.AppendToMapping(nameof(Entry), (handler, view) =>
            {
#if ANDROID
            handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#endif
            });

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

            builder.Services.AddTransient<ServiceUserInitialEditProfilePage>();
            builder.Services.AddTransient<ServiceUserInitialEditProfileViewModel>();

            builder.Services.AddTransient<ServiceProviderInitialEditProfilePage>();
            builder.Services.AddTransient<ServiceProviderInitialEditProfileViewModel>();

            builder.Services.AddTransient<ServiceProviderProfilePage>();
            builder.Services.AddTransient<ServiceProviderProfileViewModel>();

            builder.Services.AddTransient<ServiceUserProfilePage>();
            builder.Services.AddTransient<ServiceUserProfileViewModel>();
          
            builder.Services.AddTransient<ServiceUserPremisesPage>();
            builder.Services.AddTransient<ServiceUserPremisesViewModel>();

            builder.Services.AddTransient<ServiceUserAddPremisePage>();
            builder.Services.AddTransient<ServiceUserAddPremiseViewModel>();

            builder.Services.AddTransient<ServiceUserEditPremisePage>();
            builder.Services.AddTransient<ServiceUserEditPremiseViewModel>();

            builder.Services.AddTransient<ServiceProviderEditServicesPage>();
            builder.Services.AddTransient<ServiceProviderEditServicesViewModel>();

            builder.Services.AddTransient<ServiceProviderSelectServicesPage>();
            builder.Services.AddTransient<ServiceProviderSelectServicesViewModel>();

            builder.Services.AddTransient<ServiceUserConfirmPremiseSelectionPage>();
            builder.Services.AddTransient<ServiceUserConfirmPremiseSelectionViewModel>();

            builder.Services.AddTransient<ServiceUserSelectPremisePage>();
            builder.Services.AddTransient<ServiceUserSelectPremiseViewModel>();

            builder.Services.AddTransient<ServiceUserReadPremisePage>();
            builder.Services.AddTransient<ServiceUserReadPremiseViewModel>();

            builder.Services.AddTransient<ServiceUserRequestsPage>();
            builder.Services.AddTransient<ServiceUserRequestsViewModel>();

            builder.Services.AddTransient<ServiceUserAddRequestPage>();
            builder.Services.AddTransient<ServiceUserAddRequestViewModel>();

            builder.Services.AddTransient<ServiceUserAddRequestNextPage>();
            builder.Services.AddTransient<ServiceUserAddRequestNextViewModel>();

            builder.Services.AddTransient<ServiceUserEditRequestPage>();
            builder.Services.AddTransient<ServiceUserEditRequestViewModel>();

            builder.Services.AddTransient<ServiceUserEditRequestNextPage>();
            builder.Services.AddTransient<ServiceUserEditRequestNextViewModel>();

            builder.Services.AddSingleton<IHttpService, HttpService>();
            builder.Services.AddSingleton<IAuthService, AuthService>();
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<IUserProfileService, UserProfileService>();
            builder.Services.AddSingleton<IProviderService, ProviderService>();
            builder.Services.AddSingleton<IPremiseService, PremiseService>();
            builder.Services.AddSingleton<ICleaningService, CleaningService>();
            builder.Services.AddSingleton<IUserRequestService, UserRequestService>();

            return builder.Build();
        }
    }
}