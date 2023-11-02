using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserEditPremisePage : ContentPage
{
	public ServiceUserEditPremisePage(ServiceUserEditPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}