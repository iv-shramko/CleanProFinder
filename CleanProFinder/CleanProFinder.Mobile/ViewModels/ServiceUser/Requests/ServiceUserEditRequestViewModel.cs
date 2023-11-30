using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.CleaningServices;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
[QueryProperty(nameof(Description), nameof(Description))]
[QueryProperty(nameof(RequestId), nameof(RequestId))]
public partial class ServiceUserEditRequestViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserEditRequestViewModel(
        IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private string _premiseId;

    [ObservableProperty]
    private string _premiseAddress;

    [ObservableProperty]
    private float _square;

    [ObservableProperty]
    private ObservableCollection<CleaningServiceDto> _services;

    [ObservableProperty]
    private string _serviceProviderId;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private bool _isCanceled;

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
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserPremiseInfoViewModel.PremiseId), PremiseId }
        };

        await Shell.Current.GoToAsync($"{nameof(ServiceUserPremiseInfoPage)}", navigationParameters);
    }

    private async void LoadRequest(string requestId)
    {
        var response = await _requestService.GetServiceUserRequestAsync(requestId);

        if (response.IsSuccess)
        {
            PremiseId = response.Result.PremiseId.ToString();
            PremiseAddress = response.Result.Address;
            Square = response.Result.Square;
            PremiseAddress = response.Result.Address;
            Services = new ObservableCollection<CleaningServiceDto>(response.Result.Services);
            Description = response.Result.Description;
            Square = response.Result.Square;
            ServiceProviderId = response.Result.ProviderId.ToString();
            IsCanceled = response.Result.Status == "Canceled";
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise Failed", response.Error);
    }

    [RelayCommand]
    private async Task NextStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserEditRequestNextViewModel.RequestId), RequestId },
            { nameof(ServiceUserEditRequestNextViewModel.Description), Description },
            { nameof(ServiceUserEditRequestNextViewModel.ServiceProviderId), Description },
            { nameof(ServiceUserEditRequestNextViewModel.IsCanceled), IsCanceled }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserEditRequestNextPage), navigationParameters);
    }
}
