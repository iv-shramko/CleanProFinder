using CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

namespace CleanProFinder.Mobile.Views.ServiceProvider.Requests;

public partial class ServiceProviderRequestNextPage : ContentPage
{
	public ServiceProviderRequestNextPage(ServiceProviderRequestNextViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}