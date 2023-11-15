using CleanProFinder.Mobile.ViewModels.ServiceUser.Profile;

namespace CleanProFinder.Mobile.Views.ServiceUser.Profile;

public partial class ServiceUserInitialEditProfilePage : ContentPage
{
    public ServiceUserInitialEditProfilePage(ServiceUserInitialEditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}