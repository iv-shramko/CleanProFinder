using CleanProFinder.Mobile.ViewModels;
namespace CleanProFinder.Mobile.Views;

public partial class EditProfilePage : ContentPage
{
	public EditProfilePage(EditProfileViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}