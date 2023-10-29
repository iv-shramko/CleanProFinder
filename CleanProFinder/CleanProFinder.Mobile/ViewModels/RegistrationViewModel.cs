using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

[QueryProperty(nameof(IsCustomer), nameof(IsCustomer))]
public partial class RegistrationViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IAuthService _authService;

    public RegistrationViewModel(IDialogService dialogService, IAuthService authService)
    {
        _dialogService = dialogService;
        _authService = authService;
    }

    [ObservableProperty]
    private bool _isCustomer;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task SignUp()
    {
        var response = await _authService.SignUpAsync(Email, Password);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync(IsCustomer
                ? "//CustomerStartingPage"
                : "//ServiceProviderStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Sign Up Failed", response.Error);
    }
}