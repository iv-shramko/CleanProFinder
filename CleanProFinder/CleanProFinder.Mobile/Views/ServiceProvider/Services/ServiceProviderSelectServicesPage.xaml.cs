using CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Services;

public partial class ServiceProviderSelectServicesPage : ContentPage
{
    public ServiceProviderSelectServicesPage(ServiceProviderSelectServicesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceProviderSelectServicesViewModel viewModel)
        {
            await viewModel.LoadServices();
        }
    }
}