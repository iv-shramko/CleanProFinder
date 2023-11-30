using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Services;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.CleaningServices;
using CleanProFinder.Shared.Dto.Premises;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserAddRequestViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;
    private readonly IPremiseService _premiseService;
    private readonly ICleaningService _cleaningService;
    private readonly IRequestStorage _requestStorage;

    public ServiceUserAddRequestViewModel(
        IDialogService dialogService, IRequestService requestService,
        IPremiseService premiseService, ICleaningService cleaningService,
        IRequestStorage requestStorage)
    {
        _dialogService = dialogService;
        _requestService = requestService;
        _premiseService = premiseService;
        _cleaningService = cleaningService;
        _requestStorage = requestStorage;

        _services = new ObservableCollection<CleaningServiceDto>(_requestStorage.Services);
        PremiseId = _requestStorage.PremiseId;
    }

    {
        _dialogService = dialogService;
        _services = new ObservableCollection<CleaningServiceDto>();
    }

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _services;

    [ObservableProperty]
    private OwnPremiseFullInfoDto _selectedPremise;

    [ObservableProperty]
    private string _premiseAddress;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _requestStorage.PremiseId = premiseId;

        var payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        if (query.TryGetValue(nameof(SelectedPremise), out var newSelectedPremise))
        {
            SelectedPremise = (OwnPremiseFullInfoDto)newSelectedPremise;
        }

        if (response.IsSuccess)
        {
            PremiseAddress = response.Result.Address;
            Square = response.Result.Square;

            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise Failed", response.Error);
    }

    [RelayCommand]
    private async Task SelectPremise()
    {
        await Shell.Current.GoToAsync(nameof(ServiceUserSelectPremisePage));
    }

    [RelayCommand]
    private async Task ViewPremise()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserPremiseInfoViewModel.PremiseId), SelectedPremise.Id }
        };

        await Shell.Current.GoToAsync($"{nameof(ServiceUserPremiseInfoPage)}", navigationParameters);
    }

    [RelayCommand]
    private async Task SelectServices()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserSelectServicesViewModel.ExistingServices), Services }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserSelectServicesPage), navigationParameters);
    }

    [RelayCommand]
    private void RemoveService(CleaningServiceDto service)
    {
        Services.Remove(service);
    }

    [RelayCommand]
    private async Task NextStep()
    {
        _requestStorage.PremiseId = PremiseId;
        _requestStorage.Services = Services.ToList();

        await Shell.Current.GoToAsync(nameof(ServiceUserAddRequestNextPage));
    }
}
