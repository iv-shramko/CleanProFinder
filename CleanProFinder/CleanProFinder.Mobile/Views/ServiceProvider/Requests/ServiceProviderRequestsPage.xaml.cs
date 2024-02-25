using CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Requests;

public partial class ServiceProviderRequestsPage : ContentPage
{
	public ServiceProviderRequestsPage(ServiceProviderRequestsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceProviderRequestsViewModel viewModel)
        {
            viewModel.IsRefreshing = true;
        }
    }
}