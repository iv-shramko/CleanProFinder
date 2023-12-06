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
    private readonly ISavedProviderService _savedProviderService;
    private readonly IDialogService _dialogService;
    private readonly IProviderService _providerService;

    public ServiceUserSavedProvidersViewModel(ISavedProviderService savedProviderService, IDialogService dialogService,
        IProviderService providerService) 
    {
        _savedProviderService = savedProviderService;
        _dialogService = dialogService;
        _providerService = providerService;
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

        var response = await _savedProviderService.GetProvidersAsync();
        
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
        var response = await _savedProviderService.DeleteProviderAsync(provider.Id);

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
