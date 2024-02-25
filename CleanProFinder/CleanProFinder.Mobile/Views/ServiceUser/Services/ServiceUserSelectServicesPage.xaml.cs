using CleanProFinder.Mobile.ViewModels.ServiceUser.Services;

namespace CleanProFinder.Mobile.Views.ServiceUser.Services;

public partial class ServiceUserSelectServicesPage : ContentPage
{
	public ServiceUserSelectServicesPage(ServiceUserSelectServicesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserSelectServicesViewModel viewModel)
        {
            await viewModel.LoadServices();
        }
    }
}