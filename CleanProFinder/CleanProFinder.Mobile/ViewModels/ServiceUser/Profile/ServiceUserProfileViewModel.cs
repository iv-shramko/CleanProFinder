using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Profile;

public partial class ServiceUserProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public ServiceUserProfileViewModel(IDialogService dialogService, IUserProfileService userProfileService,
        IAuthService authService)
    {
        _authService = authService;
        _dialogService = dialogService;
        _userProfileService = userProfileService;
        IsEditing = false;
    }

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private bool _isEditing;

    [RelayCommand]
    private void StartEditing()
    {
        IsEditing = true;
    }

    public async Task LoadProfile()
    {
        var response = await _userProfileService.GetServiceUserProfileAsync();

        if (response.IsSuccess)
        {
            FirstName = response.Result.FirstName;
            LastName = response.Result.LastName;
            PhoneNumber = response.Result.PhoneNumber;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }

    [RelayCommand]
    private async Task UpdateProfile()
    {
        var response = await _userProfileService.EditServiceUserProfileAsync(FirstName, LastName, PhoneNumber);

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Profile updated successfully",
                "You have successfully updated your profile.", "Ok");
            IsEditing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }

    [RelayCommand]
    private async Task Logout()
    {
        _authService.Logout();
        await Shell.Current.GoToAsync("//RolePage");
    }
}
