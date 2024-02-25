using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

namespace CleanProFinder.Mobile.Views.ServiceUser.Providers;

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