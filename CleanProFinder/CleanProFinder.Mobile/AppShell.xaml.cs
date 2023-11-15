using CleanProFinder.Mobile.ViewModels;
using CleanProFinder.Mobile.Views.Authentication;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;

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
        }
    }
}