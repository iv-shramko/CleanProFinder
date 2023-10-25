using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Mobile.Views;

namespace CleanProFinder.Mobile.ViewModels;

public partial class RoleViewModel : ObservableObject
{
    [RelayCommand]
    private async Task OnCustomerRoleClicked()
    {
    }

    [RelayCommand]
    private async Task OnServiceProviderRoleClicked()
    {
    }

    [RelayCommand]
    private async Task OnSignInClicked()
    {
    }
}
