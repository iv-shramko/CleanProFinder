using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class AppShellViewModel : ObservableObject
{
    private readonly IAuthService _authService;

    public AppShellViewModel(IAuthService authService)
    {
        _authService = authService;
    }

    [ObservableProperty]
    private bool _isServiceUserTabVisible = true;

    [ObservableProperty]
    private bool _isServiceProviderTabVisible = true;

    [RelayCommand]
    private void SetTabVisibility()
    {
        if (_authService is not { IsAuthenticated: true })
        {
            return;
        }

        IsServiceUserTabVisible = _authService.IsServiceUser;
        IsServiceProviderTabVisible = !_authService.IsServiceUser;
    }
}