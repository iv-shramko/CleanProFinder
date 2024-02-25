using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.CleaningServices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Services;

[QueryProperty(nameof(ExistingServices), nameof(ExistingServices))]
public partial class ServiceUserSelectServicesViewModel : ObservableObject
{
    private readonly ICleaningService _cleaningService;

    public ServiceUserSelectServicesViewModel(ICleaningService cleaningService)
    {
        _cleaningService = cleaningService;
        _availableServices = new ObservableCollection<CleaningServiceDto>();
        _selectedServices = new ObservableCollection<object>();
    }

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _availableServices;

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _existingServices;

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
                if (ExistingServices.All(existing => existing.Id != service.Id))
                {
                    AvailableServices.Add(service);
                }
            }
        }
    }

    [RelayCommand]
    private async Task ConfirmSelection()
    {
        var selectedServices = new ObservableCollection<CleaningServiceDto>(SelectedServices.OfType<CleaningServiceDto>());
        var services = ExistingServices.Concat(selectedServices).ToList();

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestViewModel.Services), services }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
