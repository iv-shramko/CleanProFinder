using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

[QueryProperty(nameof(IsServiceUser), nameof(IsServiceUser))]
public partial class InitialEditProfileViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public InitialEditProfileViewModel(IDialogService dialogService, IUserProfileService userProfileService)
    {
        _dialogService = dialogService;
        _userProfileService = userProfileService;
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
    private string _websiteUrl;

    [ObservableProperty] 
    private Image _logoImage = new() { Source = "photo_placeholder.svg" };

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
            response = await _userProfileService.EditServiceProviderProfileAsync(ServiceProviderName, String.Empty, PhoneNumber, WebsiteUrl);
        }

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync(IsServiceUser
                ? "//ServiceUserStartingPage"
                : "//ServiceProviderStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }
}