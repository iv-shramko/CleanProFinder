using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

namespace CleanProFinder.Mobile.Views.ServiceUser.Requests;

public partial class ServiceUserSelectServiceProvidersPage : ContentPage
{
    public ServiceUserSelectServiceProvidersPage(ServiceUserSelectServiceProvidersViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserSelectServiceProvidersViewModel viewModel)
        {
            await viewModel.LoadServiceProviders();
        }
    }
}