using CleanProFinder.Mobile.ViewModels;
namespace CleanProFinder.Mobile.Views;

public partial class InitialEditProfilePage : ContentPage
{
    public InitialEditProfilePage(InitialEditProfileViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}