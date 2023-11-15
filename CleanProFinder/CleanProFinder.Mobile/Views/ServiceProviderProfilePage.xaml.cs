using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceProviderProfilePage : ContentPage
{
    public ServiceProviderProfilePage(ServiceProviderProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceProviderProfileViewModel viewModel)
        {
            await viewModel.LoadProfile();
        }
    }
}