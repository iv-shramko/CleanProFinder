using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Services;

[QueryProperty(nameof(ExistingServices), nameof(ExistingServices))]
public partial class ServiceProviderSelectServicesViewModel : ObservableObject
{
    private readonly ICleaningService _cleaningService;

    public ServiceProviderSelectServicesViewModel(ICleaningService cleaningService)
    {
        _cleaningService = cleaningService;
        _availableServices = new ObservableCollection<CleaningServiceDto>();
        _existingServices = new ObservableCollection<ProviderServiceFullInfoDto>();
        _selectedServices = new ObservableCollection<object>();
    }

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _availableServices;

    [ObservableProperty]
    private ObservableCollection<ProviderServiceFullInfoDto> _existingServices;

    [ObservableProperty]
    private ObservableCollection<object> _selectedServices;

    [RelayCommand]
    public async Task LoadServices()
    {
        var response = await _cleaningService.GetServicesAsync();
        
        if (response.IsSuccess)
        {
            foreach (var service in response.Result)
            {
                if (ExistingServices.All(selected => selected.CleaningServiceId != service.Id))
                {
                    AvailableServices.Add(service);
                }
            }
        }
    }

    [RelayCommand]
    private async Task ConfirmSelection()
    {
        var selectedServices = SelectedServices.OfType<CleaningServiceDto>();

        var selectedServicesFullInfo = selectedServices.Select(service => new ProviderServiceFullInfoDto
        {
            CleaningServiceId = service.Id, 
            Name = service.Name
        }).ToList();

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderEditServicesViewModel.Services), selectedServicesFullInfo }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}