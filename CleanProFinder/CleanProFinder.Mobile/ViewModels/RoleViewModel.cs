using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Mobile.Views;

namespace CleanProFinder.Mobile.ViewModels;

public partial class RoleViewModel : ObservableObject
{
    [RelayCommand]
    private async Task OnCustomerRoleClicked()
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?{nameof(RegistrationViewModel.IsCustomer)}={true}");
    }

    [RelayCommand]
    private async Task OnServiceProviderRoleClicked()
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?{nameof(RegistrationViewModel.IsCustomer)}={false}");
    }

    [RelayCommand]
    private async Task OnSignInClicked()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }
}
