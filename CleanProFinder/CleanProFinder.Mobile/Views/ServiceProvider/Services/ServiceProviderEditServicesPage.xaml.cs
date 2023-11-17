using CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Services;

public partial class ServiceProviderEditServicesPage : ContentPage
{
    public ServiceProviderEditServicesPage(ServiceProviderEditServicesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}