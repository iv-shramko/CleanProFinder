using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views.Authentication;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Providers;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Services;

namespace CleanProFinder.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell(AppShellViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;

            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(ServiceUserInitialEditProfilePage), typeof(ServiceUserInitialEditProfilePage));
            Routing.RegisterRoute(nameof(ServiceProviderInitialEditProfilePage), typeof(ServiceProviderInitialEditProfilePage));
            Routing.RegisterRoute(nameof(ServiceUserAddPremisePage), typeof(ServiceUserAddPremisePage));
            Routing.RegisterRoute(nameof(ServiceUserEditPremisePage), typeof(ServiceUserEditPremisePage));
            Routing.RegisterRoute(nameof(ServiceProviderEditServicesPage), typeof(ServiceProviderEditServicesPage));
            Routing.RegisterRoute(nameof(ServiceProviderSelectServicesPage), typeof(ServiceProviderSelectServicesPage));
            Routing.RegisterRoute(nameof(ServiceUserSelectPremisePage), typeof(ServiceUserSelectPremisePage));
            Routing.RegisterRoute(nameof(ServiceUserConfirmPremiseSelectionPage), typeof(ServiceUserConfirmPremiseSelectionPage));
            Routing.RegisterRoute(nameof(PremiseInfoPage), typeof(PremiseInfoPage));
            Routing.RegisterRoute(nameof(ServiceUserAddRequestPage), typeof(ServiceUserAddRequestPage));
            Routing.RegisterRoute(nameof(ServiceUserAddRequestNextPage), typeof(ServiceUserAddRequestNextPage));
            Routing.RegisterRoute(nameof(ServiceUserEditRequestPage), typeof(ServiceUserEditRequestPage));
            Routing.RegisterRoute(nameof(ServiceUserEditRequestNextPage), typeof(ServiceUserEditRequestNextPage));
            Routing.RegisterRoute(nameof(ServiceUserSelectServicesPage), typeof(ServiceUserSelectServicesPage));
            Routing.RegisterRoute(nameof(ServiceUserSelectServiceProvidersPage), typeof(ServiceUserSelectServiceProvidersPage));
            Routing.RegisterRoute(nameof(ServiceProviderInfoPage), typeof(ServiceProviderInfoPage));
            Routing.RegisterRoute(nameof(ServiceProviderActiveRequestPage), typeof(ServiceProviderActiveRequestPage));
            Routing.RegisterRoute(nameof(ServiceProviderActiveRequestNextPage), typeof(ServiceProviderActiveRequestNextPage));
            Routing.RegisterRoute(nameof(ServiceUserSavedProvidersPage), typeof(ServiceUserSavedProvidersPage));
        }
    }
}