using CleanProFinder.Mobile.ViewModels.Authentication;

namespace CleanProFinder.Mobile.Views.Authentication;

public partial class RolePage : ContentPage
{
	public RolePage(RoleViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
	}
}
