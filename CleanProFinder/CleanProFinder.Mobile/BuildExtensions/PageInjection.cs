using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.ViewModels.Authentication;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.Profile;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;
using CleanProFinder.Mobile.ViewModels.ServiceUser;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Profile;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Services;
using CleanProFinder.Mobile.Views.Authentication;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Mobile.Views.ServiceUser;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Providers;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Services;

namespace CleanProFinder.Mobile.BuildExtensions;

internal static class PageInjection
{
    internal static void AddPages(this IServiceCollection services)
    {
        services.AddSingleton<AppShellViewModel>();

        services.AddTransient<RolePage>();
        services.AddTransient<RoleViewModel>();

        services.AddTransient<RegistrationPage>();
        services.AddTransient<RegistrationViewModel>();

        services.AddTransient<LoginPage>();
        services.AddTransient<LoginViewModel>();

        services.AddTransient<ServiceUserStartingPage>();
        services.AddTransient<ServiceUserStartingViewModel>();

        services.AddTransient<ServiceProviderStartingPage>();
        services.AddTransient<ServiceProviderStartingViewModel>();

        services.AddTransient<ServiceUserInitialEditProfilePage>();
        services.AddTransient<ServiceUserInitialEditProfileViewModel>();

        services.AddTransient<ServiceProviderInitialEditProfilePage>();
        services.AddTransient<ServiceProviderInitialEditProfileViewModel>();

        services.AddTransient<ServiceProviderProfilePage>();
        services.AddTransient<ServiceProviderProfileViewModel>();

        services.AddTransient<ServiceUserProfilePage>();
        services.AddTransient<ServiceUserProfileViewModel>();
      
        services.AddTransient<ServiceUserPremisesPage>();
        services.AddTransient<ServiceUserPremisesViewModel>();

        services.AddTransient<ServiceUserAddPremisePage>();
        services.AddTransient<ServiceUserAddPremiseViewModel>();

        services.AddTransient<ServiceUserEditPremisePage>();
        services.AddTransient<ServiceUserEditPremiseViewModel>();

        services.AddTransient<ServiceProviderEditServicesPage>();
        services.AddTransient<ServiceProviderEditServicesViewModel>();

        services.AddTransient<ServiceProviderSelectServicesPage>();
        services.AddTransient<ServiceProviderSelectServicesViewModel>();

        services.AddTransient<ServiceUserConfirmPremiseSelectionPage>();
        services.AddTransient<ServiceUserConfirmPremiseSelectionViewModel>();

        services.AddTransient<ServiceUserSelectPremisePage>();
        services.AddTransient<ServiceUserSelectPremiseViewModel>();

        services.AddTransient<PremiseInfoPage>();
        services.AddTransient<PremiseInfoViewModel>();

        services.AddTransient<ServiceUserRequestsPage>();
        services.AddTransient<ServiceUserRequestsViewModel>();

        services.AddTransient<ServiceUserAddRequestPage>();
        services.AddTransient<ServiceUserAddRequestViewModel>();

        services.AddTransient<ServiceUserAddRequestNextPage>();
        services.AddTransient<ServiceUserAddRequestNextViewModel>();

        services.AddTransient<ServiceUserEditRequestPage>();
        services.AddTransient<ServiceUserEditRequestViewModel>();

        services.AddTransient<ServiceUserEditRequestNextPage>();
        services.AddTransient<ServiceUserEditRequestNextViewModel>();

        services.AddTransient<ServiceUserSelectServicesPage>();
        services.AddTransient<ServiceUserSelectServicesViewModel>();

        services.AddTransient<ServiceUserSelectServiceProvidersPage>();
        services.AddTransient<ServiceUserSelectServiceProvidersViewModel>();

        services.AddTransient<ServiceProviderInfoPage>();
        services.AddTransient<ServiceProviderInfoViewModel>();

        services.AddTransient<ServiceProviderActiveRequestPage>();
        services.AddTransient<ServiceProviderActiveRequestViewModel>();

        services.AddTransient<ServiceProviderActiveRequestNextPage>();
        services.AddTransient<ServiceProviderActiveRequestNextViewModel>();

        services.AddTransient<ServiceUserSelectProviderOfferPage>();
        services.AddTransient<ServiceUserSelectProviderOfferViewModel>();

        services.AddTransient<ServiceUserSavedProvidersPage>();
        services.AddTransient<ServiceUserSavedProvidersViewModel>();
    }
}
