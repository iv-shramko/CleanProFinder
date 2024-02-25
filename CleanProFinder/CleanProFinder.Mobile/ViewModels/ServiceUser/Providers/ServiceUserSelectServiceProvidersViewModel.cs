using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Profile;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;

[QueryProperty(nameof(ExistingProviders), nameof(ExistingProviders))]
public partial class ServiceUserSelectServiceProvidersViewModel : ObservableObject
{
    private readonly IProviderService _providerService;

    public ServiceUserSelectServiceProvidersViewModel(IProviderService providerService)
    {
        _providerService = providerService;
        _availableProviders = new ObservableCollection<ProviderPreviewDto>();
        _existingProviders = new ObservableCollection<ProviderRequestInteractionInfo>();
        _selectedProviders = new ObservableCollection<object>();
    }

    [ObservableProperty]
    private ObservableCollection<ProviderPreviewDto> _availableProviders;

    [ObservableProperty]
    private ObservableCollection<ProviderRequestInteractionInfo> _existingProviders;

    [ObservableProperty]
    private ObservableCollection<object> _selectedProviders;

    [RelayCommand]
    public async Task LoadServiceProviders()
    {
        var response = await _providerService.GetServiceProvidersAsync();

        if (response.IsSuccess)
        {
            foreach (var provider in response.Result)
            {
                if (ExistingProviders.All(existing => existing.ProviderId != provider.Id))
                {
                    AvailableProviders.Add(provider);
                }
            }
        }
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
    private async Task ConfirmSelection()
    {
        var selectedProviders = SelectedProviders.OfType<ProviderPreviewDto>();

        var selectedProvidersInteractionInfo = selectedProviders.Select(provider => new ProviderRequestInteractionInfo
        {
            ProviderId = provider.Id,
            ProviderName = provider.Name
        }).ToList();

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestNextViewModel.ServiceProviders), selectedProvidersInteractionInfo }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}