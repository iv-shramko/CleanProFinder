using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

namespace CleanProFinder.Mobile.Views.ServiceUser.Providers;

public partial class ServiceUserServiceProviderInfoPage : ContentPage
{
    public ServiceUserServiceProviderInfoPage(ServiceUserServiceProviderInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}