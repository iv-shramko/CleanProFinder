using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserAddRequestNextPage : ContentPage
{
	public ServiceUserAddRequestNextPage(ServiceUserAddRequestNextViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}