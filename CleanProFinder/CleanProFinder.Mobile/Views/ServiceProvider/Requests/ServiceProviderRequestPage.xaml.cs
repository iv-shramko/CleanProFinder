using CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Requests;

public partial class ServiceProviderRequestPage : ContentPage
{
    public ServiceProviderRequestPage(ServiceProviderRequestViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}