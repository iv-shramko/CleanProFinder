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
            await Shell.Current.GoToAsync(_authService.IsServiceUser
                ? "//ServiceUserStartingPage"
                : "//ServiceProviderStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Sign In Failed", response.Error);
    }
}