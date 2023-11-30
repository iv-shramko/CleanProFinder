using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserAddRequestNextPage : ContentPage
{
	public ServiceUserAddRequestNextPage(ServiceUserAddRequestNextViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnAppearing()
    {
        if (BindingContext is ServiceUserAddRequestNextViewModel viewModel)
        {
            viewModel.Description = viewModel.Request.Description;
        }

        base.OnAppearing();
    }
}