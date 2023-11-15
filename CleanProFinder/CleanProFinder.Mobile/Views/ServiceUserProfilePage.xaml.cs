using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserProfilePage : ContentPage
{
    public ServiceUserProfilePage(ServiceUserProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is ServiceUserProfileViewModel viewModel)
        {
            await viewModel.LoadProfile();
        }
    }
}