﻿using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Profile;

public partial class ServiceProviderProfileViewModel : ObservableObject
{
    private readonly IAuthService _authService;
    private readonly IDialogService _dialogService;
    private readonly IUserProfileService _userProfileService;

    public ServiceProviderProfileViewModel(IDialogService dialogService, IUserProfileService userProfileService,
        IAuthService authService)
    {
        _authService = authService;
        _dialogService = dialogService;
        _userProfileService = userProfileService;
        _services = new ObservableCollection<ProviderServiceFullInfoDto>();
        IsEditing = false;
    }

    [ObservableProperty] 
    private string _email;

    [ObservableProperty] 
    private string _name;

    [ObservableProperty] 
    private string _phoneNumber;

    [ObservableProperty] 
    private string _description;

    [ObservableProperty] 
    private string _websiteUrl;

    [ObservableProperty] 
    private Image _logoImage;

    [ObservableProperty] 
    private ObservableCollection<ProviderServiceFullInfoDto> _services;

    [ObservableProperty] 
    private bool _isEditing;

    [RelayCommand]
    private void StartEditing()
    {
        IsEditing = true;
    }

    public async Task LoadProfile()
    {
        var response = await _userProfileService.GetServiceProviderProfileAsync();

        if (response.IsSuccess)
        {
            Email = response.Result.Email;
            Name = response.Result.Name;
            PhoneNumber = response.Result.PhoneNumber;
            Description = response.Result.Description;
            WebsiteUrl = response.Result.Site;
            Services = new ObservableCollection<ProviderServiceFullInfoDto>(response.Result.Services);
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Profile Edit Failed", response.Error);
    }

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
    private async Task EditServices()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {  nameof(ServiceProviderEditServicesViewModel.Services), Services }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderEditServicesPage), navigationParameters);
    }

    [RelayCommand]
    private async Task UpdateProfile()
    {
        var response =
            await _userProfileService.EditServiceProviderProfileAsync(Name, Description, PhoneNumber, WebsiteUrl);

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
