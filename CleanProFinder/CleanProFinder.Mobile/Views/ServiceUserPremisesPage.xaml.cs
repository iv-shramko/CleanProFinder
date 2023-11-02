using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserPremisesPage : ContentPage
{
	public ServiceUserPremisesPage(ServiceUserPremisesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}