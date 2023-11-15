using CleanProFinder.Mobile.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceProviderInitialEditProfileViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public ServiceProviderInitialEditProfileViewModel(IDialogService dialogService,
        IUserProfileService userProfileService)
    {
        _dialogService = dialogService;
        _userProfileService = userProfileService;
    }

    [ObservableProperty]
    private string _name;

    [ObservableProperty]
    private string _phoneNumber;

    [ObservableProperty]
    private string _websiteUrl;

    [ObservableProperty] 
    private Image _logoImage = new() { Source = "photo_placeholder.svg" };

    [RelayCommand]
    private async Task AddLogoImage()
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
    private async Task UpdateProfile()
    {
        var response =
            await _userProfileService.EditServiceProviderProfileAsync(Name, string.Empty, PhoneNumber, WebsiteUrl);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceProviderStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }
}