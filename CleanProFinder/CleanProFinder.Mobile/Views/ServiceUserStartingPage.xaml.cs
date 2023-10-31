using CleanProFinder.Mobile.ViewModels;

namespace CleanProFinder.Mobile.Views;

public partial class ServiceUserStartingPage : ContentPage
{
	public ServiceUserStartingPage(ServiceUserStartingViewModel viewModel)
	{
        InitializeComponent();
        BindingContext = viewModel;
	}
}
