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
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserAddRequestViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;

    public ServiceUserAddRequestViewModel(IDialogService dialogService)
    {
        _dialogService = dialogService;
        _services = new ObservableCollection<CleaningServiceDto>();
        _request = new RequestFullInfoDto();
    }

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _services;

    [ObservableProperty]
    private OwnPremiseFullInfoDto _selectedPremise;

    [ObservableProperty]
    private RequestFullInfoDto _request;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Request), out var newRequest))
        {
            Request = (RequestFullInfoDto)newRequest;
        }

        if (query.TryGetValue(nameof(SelectedPremise), out var newSelectedPremise))
        {
            SelectedPremise = (OwnPremiseFullInfoDto)newSelectedPremise;
        }

        if (query.TryGetValue(nameof(Services), out var newServices))
        {
            var services = (ICollection<CleaningServiceDto>)newServices;

            var existingServiceIds = Services.Select(existing => existing.Id);
            var servicesToAdd = services.Where(service => !existingServiceIds.Contains(service.Id));

            foreach (var service in servicesToAdd)
            {
                Services.Add(service);
            }
        }

        query.Clear();
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

        await Shell.Current.GoToAsync(nameof(ServiceUserPremiseInfoPage), navigationParameters);
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
        if (SelectedPremise == null || !Services.Any())
        {
            await _dialogService.ShowAlertAsync("Adding Request Failed", "No premise or service was selected.", "Ok");
            return;
        }

        Request.PremiseId = SelectedPremise.Id;
        Request.Services = Services.ToList();

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestNextViewModel.Request), Request }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserAddRequestNextPage), navigationParameters);
    }
}
