using CleanProFinder.Mobile.Views;

namespace CleanProFinder.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(InitialEditProfilePage), typeof(InitialEditProfilePage));
        }
    }
}