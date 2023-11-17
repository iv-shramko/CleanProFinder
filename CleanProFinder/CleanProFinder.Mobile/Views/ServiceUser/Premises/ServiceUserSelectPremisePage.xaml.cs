using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserSelectPremisePage : ContentPage
{
	public ServiceUserSelectPremisePage(ServiceUserSelectPremiseViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}