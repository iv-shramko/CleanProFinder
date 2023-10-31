using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Mobile.Views;

namespace CleanProFinder.Mobile.ViewModels;

public partial class RoleViewModel : ObservableObject
{
    [RelayCommand]
    private async Task SignUpAsServiceUser()
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?{nameof(RegistrationViewModel.IsServiceUser)}={true}");
    }

    [RelayCommand]
    private async Task SignUpAsServiceProvider()
    {
        await Shell.Current.GoToAsync($"{nameof(RegistrationPage)}?{nameof(RegistrationViewModel.IsServiceUser)}={false}");
    }

    [RelayCommand]
    private async Task SignIn()
    {
        await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
    }
}
