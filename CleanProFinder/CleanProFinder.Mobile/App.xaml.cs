using CleanProFinder.Mobile.Services;
using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile
{
    public partial class App : Application
    {
        public App(IAuthService authService, AppShellViewModel viewModel)
        {
            InitializeComponent();
           
            authService.Initialize();

            MainPage = new AppShell(viewModel);

            if (authService.IsAuthenticated)
            {
                Shell.Current.GoToAsync(authService.IsServiceUser
                    ? "//ServiceUserStartingPage"
                    : "//ServiceProviderStartingPage");
            }
            else
            {
                Shell.Current.GoToAsync("//RolePage");
            }
        }
    }
}