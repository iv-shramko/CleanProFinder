using CleanProFinder.Mobile.ViewModels.Info;

namespace CleanProFinder.Mobile.Views.Info;

public partial class ServiceProviderInfoPage : ContentPage
{
    public ServiceProviderInfoPage(ServiceProviderInfoViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}