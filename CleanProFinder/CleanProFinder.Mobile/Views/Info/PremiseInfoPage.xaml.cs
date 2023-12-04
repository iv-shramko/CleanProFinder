using CleanProFinder.Mobile.ViewModels.Info;

namespace CleanProFinder.Mobile.Views.Info;

public partial class PremiseInfoPage : ContentPage
{
	public PremiseInfoPage(PremiseInfoViewModel infoViewModel)
	{
		InitializeComponent();
		BindingContext = infoViewModel;
	}
}