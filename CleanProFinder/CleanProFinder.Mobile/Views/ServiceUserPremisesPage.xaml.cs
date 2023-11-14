using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserPremisesPage : ContentPage
{
	public ServiceUserPremisesPage(ServiceUserPremisesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserPremisesViewModel viewModel)
        {
            viewModel.IsRefreshing = true;
        }
    }
}