using CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;

public partial class ServiceProviderStartingPage : ContentPage
{
	public ServiceProviderStartingPage(ServiceProviderStartingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceProviderStartingViewModel viewModel)
        {
            viewModel.IsRefreshing = true;
        }
    }
}