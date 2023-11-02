using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class EditProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public EditProfileViewModel(IDialogService dialogService, IUserProfileService userProfileService, IAuthService authService)
    {
        _dialogService = dialogService;
        _userProfileService = userProfileService;
        _authService = authService;
        IsServiceUser = _authService.IsServiceUser;
        IsEditing = false;
        LoadProfile();
    }

    [ObservableProperty]
    private bool _isServiceUser;

    [ObservableProperty]
    private string _firstName;

    [ObservableProperty]
    private string _lastName;

    [ObservableProperty]
    private string _serviceProviderName;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _Description;

    [ObservableProperty]
    private string _websiteUrl;

    [ObservableProperty]
    private Image _logoImage = new() { Source = "photo_placeholder.svg" };

    [ObservableProperty]
    private bool _isEditing;

    [RelayCommand]
    private async void StartEditing()
    {
        IsEditing = true;
    }

    private async void LoadProfile()
    {
        if (IsServiceUser)
        {
            ServiceResponse<UserProfileViewInfoDto> response = await _userProfileService.GetServiceUserProfileAsync();

            if (response.IsSuccess)
            {
                FirstName = response.Result.FirstName;
                LastName = response.Result.LastName;
                PhoneNumber = response.Result.PhoneNumber;
                return;
            }

            await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
        }
        else
        {
            ServiceResponse<ProviderProfileViewInfoDto> response = await _userProfileService.GetServiceProviderProfileAsync();

            if (response.IsSuccess)
            {
                ServiceProviderName = response.Result.Name;
                PhoneNumber = response.Result.PhoneNumber;
                Description = response.Result.Description;
                WebsiteUrl = response.Result.Site;
                return;
            }

            await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
        }
    }

    [RelayCommand]
    public async void AddLogoImage()
    {
        var result = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images
        });

        if (result == null)
        {
            return;
        }

        var stream = await result.OpenReadAsync();

        LogoImage = new Image
        {
            Source = ImageSource.FromStream(() => stream)
        };
    }

    [RelayCommand]
    public async void UpdateProfile()
    {
        ServiceResponse response;

        if (IsServiceUser)
        {
            response = await _userProfileService.EditServiceUserProfileAsync(FirstName, LastName, PhoneNumber);
        }
        else
        {
            response = await _userProfileService.EditServiceProviderProfileAsync(ServiceProviderName, Description, PhoneNumber, WebsiteUrl);
        }

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Profile updated successfully", String.Empty, "Ok");
            IsEditing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }

    [RelayCommand]
    public async void Logout()
    {
        SecureStorage.RemoveAll();
        await Shell.Current.GoToAsync("//RolePage");
    }
}
