using CleanProFinder.Mobile.Messages;
using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace CleanProFinder.Mobile.ViewModels;

public partial class AppShellViewModel : ObservableObject, IRecipient<UserRoleAssignedMessage>
{
    private readonly IAuthService _authService;

    public AppShellViewModel(IAuthService authService)
    {
        _authService = authService;
        WeakReferenceMessenger.Default.Register(this);
    }

    [ObservableProperty]
    private bool _isServiceUserTabBarVisible = true;

    [ObservableProperty]
    private bool _isServiceProviderTabBarVisible = true;

    public void Receive(UserRoleAssignedMessage message)
    {
        SetTabVisibility();
    }

    private void SetTabVisibility()
    {
        IsServiceUserTabBarVisible = _authService.IsServiceUser;
        IsServiceProviderTabBarVisible = !_authService.IsServiceUser;
    }
}