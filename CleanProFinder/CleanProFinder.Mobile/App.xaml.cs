using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile
{
    public partial class App : Application
    {
        public App(IAuthService authService, INotificationService notificationService, AppShellViewModel viewModel)
        {
            InitializeComponent();
            
            authService.Initialize();
            notificationService.Initialize();

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