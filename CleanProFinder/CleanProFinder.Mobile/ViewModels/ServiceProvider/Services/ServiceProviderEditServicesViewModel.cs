using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceProvider.Services;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

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
        var queryProperty = nameof(Services);

        if (!query.ContainsKey(queryProperty))
        {
            return;
        }

        if (query[queryProperty] is ICollection<ProviderServiceFullInfoDto> queryServices)
        {
            foreach (var service in queryServices)
            {
                if (Services.All(existing => existing.CleaningServiceId != service.CleaningServiceId))
                {
                    Services.Add(service);
                }
            }
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

        await Shell.Current.GoToAsync($"{nameof(ServiceProviderSelectServicesPage)}", navigationParameters);
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