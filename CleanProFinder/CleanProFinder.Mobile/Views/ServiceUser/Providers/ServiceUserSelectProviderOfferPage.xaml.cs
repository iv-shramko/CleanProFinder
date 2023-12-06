using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

namespace CleanProFinder.Mobile.Views.ServiceUser.Providers;

public partial class ServiceUserSelectProviderOfferPage : ContentPage
{
    public ServiceUserSelectProviderOfferPage(ServiceUserSelectProviderOfferViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}