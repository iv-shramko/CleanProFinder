using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

namespace CleanProFinder.Mobile.Views.ServiceUser.Premises;

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