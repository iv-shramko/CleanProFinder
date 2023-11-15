using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceProviderInitialEditProfilePage : ContentPage
{
    public ServiceProviderInitialEditProfilePage(ServiceProviderInitialEditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}