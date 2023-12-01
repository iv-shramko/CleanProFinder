using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserConfirmPremiseSelectionPage : ContentPage
{
	public ServiceUserConfirmPremiseSelectionPage(ServiceUserConfirmPremiseSelectionViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}