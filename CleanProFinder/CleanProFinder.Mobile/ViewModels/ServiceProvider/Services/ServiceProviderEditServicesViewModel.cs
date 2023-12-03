using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;

public partial class ServiceProviderEditServicesViewModel : ObservableObject, IQueryAttributable
{
    private readonly ICleaningService _cleaningService;
    private readonly IDialogService _dialogService;

    public ServiceProviderEditServicesViewModel(ICleaningService cleaningService, IDialogService dialogService)
    {
        _cleaningService = cleaningService;
        _dialogService = dialogService;
        _services = new ObservableCollection<ProviderServiceFullInfoDto>();
    }

    [ObservableProperty]
    private ObservableCollection<ProviderServiceFullInfoDto> _services;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Services), out var newServices))
        {
            var providers = (ICollection<ProviderServiceFullInfoDto>)newServices;
            var existingServiceIds = Services.Select(existing => existing.CleaningServiceId);
            var servicesToAdd = providers.Where(service => !existingServiceIds.Contains(service.CleaningServiceId)).ToList();

            Services = new ObservableCollection<ProviderServiceFullInfoDto>(servicesToAdd);
        }

        query.Clear();
    }

    [RelayCommand]
    private void DeleteService(ProviderServiceFullInfoDto service)
    {
        Services.Remove(service);
    }

    [RelayCommand]
    private async Task SelectServices()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderSelectServicesViewModel.ExistingServices), Services }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderSelectServicesPage), navigationParameters);
    }

    [RelayCommand]
    private async Task SaveServices()
    {
        var response = await _cleaningService.EditOwnServicesAsync(Services);

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Services updated successfully",
                "You have successfully updated your services.", "Ok");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Service Edit Failed", response.Error);
    }
}