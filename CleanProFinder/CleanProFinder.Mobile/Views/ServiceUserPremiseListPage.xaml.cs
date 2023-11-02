using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserPremiseListPage : ContentPage
{
	public ServiceUserPremiseListPage(ServiceUserPremiseListViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}