using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserReadPremisePage : ContentPage
{
	public ServiceUserReadPremisePage(ServiceUserReadPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}