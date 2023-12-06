using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Profile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

public partial class ServiceUserSavedProvidersViewModel : ObservableObject
{
    private readonly IProviderService _providerService;
    private readonly IDialogService _dialogService;

    public ServiceUserSavedProvidersViewModel(IProviderService providerService, IDialogService dialogService) 
    {
        _providerService = providerService;
        _dialogService = dialogService;
        _savedProviders = new ObservableCollection<ProviderPreviewDto>();
        IsRefreshing = true;
    }

    [ObservableProperty]
    private ObservableCollection<ProviderPreviewDto> _savedProviders;

    [ObservableProperty]
    private bool _isRefreshing;

    [ObservableProperty]
    private string _searchQuery;

    [RelayCommand]
    private async Task LoadServiceProviders()
    {
        IsRefreshing = true;

        var response = await _providerService.GetSavedProvidersAsync();
        
        if (response.IsSuccess)
        {
            SavedProviders = new ObservableCollection<ProviderPreviewDto>(response.Result);

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
            { nameof(ServiceProviderInfoViewModel.ProviderId), provider.Id }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderInfoPage), navigationParameters);
    }

    [RelayCommand]
    private async Task DeleteSavedProvider(ProviderPreviewDto provider)
    {
        var response = await _providerService.DeleteSavedProviderAsync(provider.Id);

        if (response.IsSuccess)
        {
            IsRefreshing = true;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Removing Provider From Favorites Failed", response.Error);
    }

    [RelayCommand]
    private void Search()
    {

    }
}
