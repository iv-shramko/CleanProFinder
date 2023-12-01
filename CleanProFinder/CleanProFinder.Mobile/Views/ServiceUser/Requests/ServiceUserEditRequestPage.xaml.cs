using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserEditRequestPage : ContentPage
{
	public ServiceUserEditRequestPage(ServiceUserEditRequestViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}