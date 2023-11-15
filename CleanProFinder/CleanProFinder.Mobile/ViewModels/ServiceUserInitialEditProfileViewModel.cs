using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceUserInitialEditProfileViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public ServiceUserInitialEditProfileViewModel(IDialogService dialogService, IUserProfileService userProfileService)
    {
        _dialogService = dialogService;
        _userProfileService = userProfileService;
    }

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _phoneNumber;

    [RelayCommand]
    private async Task UpdateProfile()
    {
        var response = await _userProfileService.EditServiceUserProfileAsync(FirstName, LastName, PhoneNumber);
        
        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }
}