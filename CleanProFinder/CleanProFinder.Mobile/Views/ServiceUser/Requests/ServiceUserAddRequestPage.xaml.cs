using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserAddRequestPage : ContentPage
{
	public ServiceUserAddRequestPage(ServiceUserAddRequestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}