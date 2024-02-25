using CleanProFinder.Mobile.ViewModels.Authentication;

namespace CleanProFinder.Mobile.Views.Authentication;

public partial class RegistrationPage : ContentPage
{
	public RegistrationPage(RegistrationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}