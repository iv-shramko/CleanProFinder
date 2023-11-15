using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserInitialEditProfilePage : ContentPage
{
    public ServiceUserInitialEditProfilePage(ServiceUserInitialEditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}