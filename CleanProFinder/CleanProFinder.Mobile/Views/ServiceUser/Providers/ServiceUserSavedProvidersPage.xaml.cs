using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

namespace CleanProFinder.Mobile.Views.ServiceUser.Providers;

public partial class ServiceUserSavedProvidersPage : ContentPage
{
	public ServiceUserSavedProvidersPage(ServiceUserSavedProvidersViewModel viewModel)
	{
        	InitializeComponent();
        	BindingContext = viewModel;
	}
}
