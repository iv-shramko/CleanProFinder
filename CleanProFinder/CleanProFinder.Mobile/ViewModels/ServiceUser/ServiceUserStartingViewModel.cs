using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Profile;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser;

public partial class ServiceUserStartingViewModel : ObservableObject
{
    private readonly IProviderService _providerService;

    public ServiceUserStartingViewModel(IProviderService providerService) 
    {
        _providerService = providerService;
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
            foreach (var serviceProvider in response.Result)
            {
                ServiceProviders.Add(serviceProvider);
            }
        }
        
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
