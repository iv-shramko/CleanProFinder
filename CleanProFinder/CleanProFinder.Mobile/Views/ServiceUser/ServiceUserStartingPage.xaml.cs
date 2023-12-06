using CleanProFinder.Mobile.ViewModels.ServiceUser;

namespace CleanProFinder.Mobile.Views.ServiceUser;

public partial class ServiceUserStartingPage : ContentPage
{
	public ServiceUserStartingPage(ServiceUserStartingViewModel viewModel)
	{
        	InitializeComponent();
        	BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserStartingViewModel viewModel)
        {
            viewModel.IsRefreshing = true;
        }
    }
}
