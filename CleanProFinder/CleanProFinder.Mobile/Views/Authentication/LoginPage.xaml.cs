using CleanProFinder.Mobile.ViewModels.Authentication;

namespace CleanProFinder.Mobile.Views.Authentication;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}