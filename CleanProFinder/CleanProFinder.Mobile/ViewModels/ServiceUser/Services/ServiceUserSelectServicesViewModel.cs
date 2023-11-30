using System.Collections.ObjectModel;
using System.Globalization;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Services;

[QueryProperty(nameof(ExistingServices), nameof(ExistingServices))]
public partial class ServiceUserSelectServicesViewModel : ObservableObject
{
    private readonly ICleaningService _cleaningService;
    private readonly IRequestStorage _requestStorage;

    public ServiceUserSelectServicesViewModel(ICleaningService cleaningService, IRequestStorage requestStorage)
    {
        _cleaningService = cleaningService;
        _requestStorage = requestStorage;

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

        AvailableServices = new ObservableCollection<CleaningServiceDto>(response.Result
            .Where(service => ExistingServices.All(existing => existing.Id != service.Id))
            .ToList());
    }

    [RelayCommand]
    private async Task ConfirmSelection()
    {
        var selectedServices = new ObservableCollection<CleaningServiceDto>(SelectedServices.OfType<CleaningServiceDto>());

        _requestStorage.Services = new List<CleaningServiceDto>(ExistingServices.Concat(selectedServices));

        await Shell.Current.GoToAsync(nameof(ServiceUserAddRequestPage));
    }
}
