using CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;

public partial class ServiceProviderActiveRequestPage : ContentPage
{
    public ServiceProviderActiveRequestPage(ServiceProviderActiveRequestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}