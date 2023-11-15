using CleanProFinder.Mobile.ViewModels.ServiceUser;

namespace CleanProFinder.Mobile.Views.ServiceUser;

public partial class ServiceUserStartingPage : ContentPage
{
	public ServiceUserStartingPage(ServiceUserStartingViewModel viewModel)
	{
        	InitializeComponent();
        	BindingContext = viewModel;
	}
}
