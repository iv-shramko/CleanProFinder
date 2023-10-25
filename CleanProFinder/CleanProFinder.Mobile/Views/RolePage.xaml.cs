using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class RolePage : ContentPage
{
	public RolePage(RoleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}