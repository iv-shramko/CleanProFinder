using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceProviderStartingPage : ContentPage
{
	public ServiceProviderStartingPage(ServiceProviderStartingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}