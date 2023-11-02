using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserAddPremisePage : ContentPage
{
	public ServiceUserAddPremisePage(ServiceUserAddPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}