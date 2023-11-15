using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserEditPremisePage : ContentPage
{
	public ServiceUserEditPremisePage(ServiceUserEditPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}