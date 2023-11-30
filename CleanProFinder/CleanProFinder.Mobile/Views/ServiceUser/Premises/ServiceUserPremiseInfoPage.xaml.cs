using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

public partial class ServiceUserPremiseInfoPage : ContentPage
{
	public ServiceUserPremiseInfoPage(ServiceUserPremiseInfoViewModel infoViewModel)
	{
		InitializeComponent();
		BindingContext = infoViewModel;
	}
}