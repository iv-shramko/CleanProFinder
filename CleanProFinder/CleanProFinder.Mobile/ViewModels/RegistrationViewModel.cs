﻿using CleanProFinder.Mobile.Services;
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
        var response = await _authService.SignUp(Email, Password);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync(IsCustomer
                ? "//CustomerStartingPage"
                : "//ServiceProviderStartingPage");
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
        
        await _dialogService.ShowAlertAsync("Sign Up Failed", errors, "OK");
    }
}