using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserAddPremisePage : ContentPage
{
	public ServiceUserAddPremisePage(ServiceUserAddPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}