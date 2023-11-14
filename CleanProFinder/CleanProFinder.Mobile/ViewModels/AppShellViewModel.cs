using CleanProFinder.Mobile.Messages;
using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace CleanProFinder.Mobile.ViewModels;

public partial class AppShellViewModel : ObservableObject, IRecipient<UserAuthenticatedMessage>
{
    private readonly IAuthService _authService;

    public AppShellViewModel(IAuthService authService)
    {
        _authService = authService;
        WeakReferenceMessenger.Default.Register(this);
    }

    [ObservableProperty]
    private bool _isServiceUserTabVisible = true;

    [ObservableProperty]
    private bool _isServiceProviderTabVisible = true;

    public void Receive(UserAuthenticatedMessage message)
    {
        SetTabVisibility();
    }

    private void SetTabVisibility()
    {
        IsServiceUserTabVisible = _authService.IsServiceUser;
        IsServiceProviderTabVisible = !_authService.IsServiceUser;
    }
}