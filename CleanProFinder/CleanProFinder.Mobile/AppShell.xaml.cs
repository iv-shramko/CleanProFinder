using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views.Authentication;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;

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
            Routing.RegisterRoute(nameof(ServiceUserReadPremisePage), typeof(ServiceUserReadPremisePage));
            Routing.RegisterRoute(nameof(ServiceUserAddRequestPage), typeof(ServiceUserAddRequestPage));
            Routing.RegisterRoute(nameof(ServiceUserAddRequestNextPage), typeof(ServiceUserAddRequestNextPage));
        }
    }
}