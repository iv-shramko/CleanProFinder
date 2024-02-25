using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserRequestsPage : ContentPage
{
	public ServiceUserRequestsPage(ServiceUserRequestsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserRequestsViewModel viewModel)
        {
            viewModel.IsRefreshing = true;
        }
    }
}