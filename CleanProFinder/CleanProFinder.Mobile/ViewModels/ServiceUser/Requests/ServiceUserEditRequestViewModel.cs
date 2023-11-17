using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
[QueryProperty(nameof(Description), nameof(Description))]
[QueryProperty(nameof(RequestId), nameof(RequestId))]
public partial class ServiceUserEditRequestViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserRequestService _userRequestService;
    private readonly IPremiseService _userPremiseService;

    public ServiceUserEditRequestViewModel(
        IDialogService dialogService, IUserRequestService userRequestsService,
        IPremiseService userPremiseService)
    {
        _dialogService = dialogService;
        _userRequestService = userRequestsService;
        _userPremiseService = userPremiseService;
    }

    [ObservableProperty]
    private string _premiseId;

    [ObservableProperty]
    private bool _hasPremiseId;

    [ObservableProperty]
    private string _premiseAddress;

    [ObservableProperty]
    private string _serviceProviderId;

    [ObservableProperty]
    private string _description;

    private string _requestId;

    public string RequestId
    {
        get => _requestId;
        set
        {
            SetProperty(ref _requestId, value);
            LoadRequest(value);
        }
    }

    [RelayCommand]
    private async Task ReadPremise()
    {
        await Shell.Current.GoToAsync(
            $"{nameof(ServiceUserReadPremisePage)}?{nameof(ServiceUserReadPremiseViewModel.PremiseId)}={PremiseId}");
    }

    private async void LoadRequest(string requestId)
    {
        var response = await _userRequestService.GetServiceUserRequestAsync(requestId);

        if (response.IsSuccess)
        {
            PremiseId = response.Result.PremiseId.ToString();
            PremiseAddress = response.Result.Address;
            Description = response.Result.Description;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise Failed", response.Error);
    }

    [RelayCommand]
    private async Task NextStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"PremiseId", PremiseId },
            {"RequestId", RequestId },
            {"Description", Description}
        };

        await Shell.Current.GoToAsync($"{nameof(ServiceUserEditRequestNextPage)}", navigationParameters);
    }
}
