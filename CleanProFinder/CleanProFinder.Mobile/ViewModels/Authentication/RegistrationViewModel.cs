using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceProvider.Profile;
using CleanProFinder.Mobile.Views.ServiceUser.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.Authentication;

[QueryProperty(nameof(IsServiceUser), nameof(IsServiceUser))]
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
    private bool _isServiceUser;

    [ObservableProperty]
    private string _email;

    [ObservableProperty]
    private string _password;

    [RelayCommand]
    private async Task SignUp()
    {
        ServiceResponse response;

        if (IsServiceUser)
        {
            response = await _authService.SignUpServiceUserAsync(Email, Password);
        }
        else
        {
            response = await _authService.SignUpServiceProviderAsync(Email, Password);
        }

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync(IsServiceUser
                ? $"{nameof(ServiceUserInitialEditProfilePage)}"
                : $"{nameof(ServiceProviderInitialEditProfilePage)}");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Sign Up Failed", response.Error);
    }
}