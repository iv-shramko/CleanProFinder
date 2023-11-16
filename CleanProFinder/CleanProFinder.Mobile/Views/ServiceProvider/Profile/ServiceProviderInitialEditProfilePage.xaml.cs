using CleanProFinder.Mobile.ViewModels.ServiceProvider.Profile;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Profile;

public partial class ServiceProviderInitialEditProfilePage : ContentPage
{
    public ServiceProviderInitialEditProfilePage(ServiceProviderInitialEditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}