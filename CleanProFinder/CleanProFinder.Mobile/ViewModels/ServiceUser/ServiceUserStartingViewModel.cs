using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;
using CleanProFinder.Mobile.Views.ServiceUser.Providers;
using CleanProFinder.Shared.Dto.Profile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser;

public partial class ServiceUserStartingViewModel : ObservableObject
{
    private readonly IProviderService _providerService;
    private readonly IDialogService _dialogService;

    public ServiceUserStartingViewModel(IProviderService providerService, IDialogService dialogService) 
    {
        _providerService = providerService;
        _dialogService = dialogService;
        _serviceProviders = new ObservableCollection<ProviderPreviewDto>();
        IsRefreshing = true;
    }

    [ObservableProperty]
    private ObservableCollection<ProviderPreviewDto> _serviceProviders;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private async Task LoadServiceProviders()
    {
        IsRefreshing = true;

        var response = await _providerService.GetServiceProvidersAsync();
        
        if (response.IsSuccess)
        {
            ServiceProviders = new ObservableCollection<ProviderPreviewDto>(response.Result);
            IsRefreshing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Service Providers Failed", response.Error);
        
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task ViewProviderDetails(ProviderPreviewDto provider)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserServiceProviderInfoViewModel.ProviderId), provider.Id }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserServiceProviderInfoPage), navigationParameters);
    }

    [RelayCommand]
    private void Search()
    {

    }
}
