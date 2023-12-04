using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

[QueryProperty(nameof(ProviderId), nameof(ProviderId))]
public partial class ServiceUserServiceProviderInfoViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IProviderService _providerService;

    public ServiceUserServiceProviderInfoViewModel(IDialogService dialogService, IProviderService providerService)
    {
        _dialogService = dialogService;
        _providerService = providerService;
    }

    private Guid _providerId;

    public Guid ProviderId
    {
        get => _providerId;
        set
        {
            SetProperty(ref _providerId, value);
            LoadProvider(value);
        }
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

    private async void LoadProvider(Guid providerId)
    {
        var response = await _providerService.GetServiceProviderAsync(providerId);

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

        await _dialogService.ShowErrorAlertAsync("Loading Service Provider Failed", response.Error);
    }
}