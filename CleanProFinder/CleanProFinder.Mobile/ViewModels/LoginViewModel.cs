using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class LoginViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IAuthService _authService;

    public LoginViewModel(IDialogService dialogService, IAuthService authService)
    {
        _dialogService = dialogService;
        _authService = authService;
    }

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task Login()
    {
        var response = await _authService.SignInAsync(Email, Password);

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Login Successful", "You are signed in", "OK");
            return;
        }

        var errors = "";

        foreach (var serviceError in response.Error.ServiceErrors)
        {
            errors += serviceError.ErrorMessage + "\n";
        }

        foreach (var validationError in response.Error.ValidationErrors)
        {
            errors += validationError.ErrorMessage + "\n";
        }
        
        await _dialogService.ShowAlertAsync("Sign In Failed", errors, "OK");
    }
}