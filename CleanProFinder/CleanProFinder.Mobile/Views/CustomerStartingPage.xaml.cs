using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class CustomerStartingPage : ContentPage
{
	public CustomerStartingPage(CustomerStartingViewModel viewModel)
	{
        	InitializeComponent();
        	BindingContext = viewModel;
	}
}
