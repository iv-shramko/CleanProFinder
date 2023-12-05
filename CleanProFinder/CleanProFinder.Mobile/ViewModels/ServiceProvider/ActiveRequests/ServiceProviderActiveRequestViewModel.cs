using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;
using CleanProFinder.Mobile.ViewModels.Info;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

[QueryProperty(nameof(RequestId), nameof(RequestId))]
public partial class ServiceProviderActiveRequestViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderActiveRequestViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    private Guid _requestId;
    public Guid RequestId
    {
        get => _requestId;
        set
        {
            SetProperty(ref _requestId, value);

            if (value != Guid.Empty)
            {
                LoadRequest(value);
            }
        }
    }

    [ObservableProperty]
    private RequestFullInfoProviderViewDto _request;

    public float Price { get; set; }

    private async void LoadRequest(Guid requestId)
    {
        var response = await _requestService.GetRequestAsync(requestId);

        if (response.IsSuccess)
        {
            Request = response.Result;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Request Failed", response.Error);
    }

    [RelayCommand]
    private async Task ViewPremise()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(PremiseInfoViewModel.PremiseId), Request.PremiseId }
        };

        await Shell.Current.GoToAsync(nameof(PremiseInfoPage), navigationParameters);
    }

    [RelayCommand]
    private async Task NextStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderActiveRequestNextViewModel.Request), Request },
            { nameof(ServiceProviderActiveRequestNextViewModel.Price), Price },
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderActiveRequestNextPage), navigationParameters);
    }
}
