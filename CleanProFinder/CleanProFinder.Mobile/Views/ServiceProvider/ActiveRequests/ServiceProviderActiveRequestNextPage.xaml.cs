using CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;

public partial class ServiceProviderActiveRequestNextPage : ContentPage
{
	public ServiceProviderActiveRequestNextPage(ServiceProviderActiveRequestNextViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}