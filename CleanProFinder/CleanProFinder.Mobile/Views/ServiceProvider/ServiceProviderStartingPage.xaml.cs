using CleanProFinder.Mobile.ViewModels.ServiceProvider;

namespace CleanProFinder.Mobile.Views.ServiceProvider;

public partial class ServiceProviderStartingPage : ContentPage
{
	public ServiceProviderStartingPage(ServiceProviderStartingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}