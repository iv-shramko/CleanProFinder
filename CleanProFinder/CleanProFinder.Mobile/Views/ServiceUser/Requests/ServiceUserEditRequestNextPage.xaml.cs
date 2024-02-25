using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserEditRequestNextPage : ContentPage
{
	public ServiceUserEditRequestNextPage(ServiceUserEditRequestNextViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}