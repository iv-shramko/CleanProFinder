using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
[QueryProperty(nameof(Description), nameof(Description))]
public partial class ServiceUserAddRequestViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserRequestService _userRequestService;
    private readonly IPremiseService _userPremiseService;
    private readonly ICleaningService _cleaningService;

    public ServiceUserAddRequestViewModel(
        IDialogService dialogService, IUserRequestService userRequestsService,
        IPremiseService userPremiseService, ICleaningService cleaningService)
    {
        _dialogService = dialogService;
        _userRequestService = userRequestsService;
        _userPremiseService = userPremiseService;
        _cleaningService = cleaningService;
    }

    private string _premiseId;

    public string PremiseId
    {
        get => _premiseId;
        set
        {
            SetProperty(ref _premiseId, value);

            if (string.IsNullOrEmpty(_premiseId))
            {
                HasPremiseId = false;
            }
            else
            {
                HasPremiseId = true;
                LoadPremise(value);
            }
        }
    }

    [ObservableProperty]
    private bool _hasPremiseId;

    [ObservableProperty]
    private string _premiseAddress;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _serviceProviderId;

    private async void LoadPremise(string premiseId)
    {
        if (string.IsNullOrEmpty(_premiseId))
        {
            return;
        }

        var payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        var response = await _userPremiseService.GetPremiseAsync(payload);

        if (response.IsSuccess)
        {
            PremiseAddress = response.Result.Address;
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
    private async Task ReadPremise()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(ServiceUserReadPremisePage)}?{nameof(ServiceUserReadPremiseViewModel.PremiseId)}={PremiseId}");
    }

    [RelayCommand]
    private async Task NextStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"PremiseId", PremiseId },
            {"Description", Description }
        };

        await Shell.Current.GoToAsync($"{nameof(ServiceUserAddRequestNextPage)}", navigationParameters);
    }
}
