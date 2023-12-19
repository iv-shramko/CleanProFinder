using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

public partial class ServiceProviderRequestNextViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderRequestNextViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private RequestFullInfoProviderViewDto _request;

    [ObservableProperty]
    private float _price;

    [ObservableProperty]
    private bool _isWaitingForAssign;

    [ObservableProperty]
    private bool _isWaitingForStart;

    [ObservableProperty]
    private bool _isWaitingForFinish;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Request), out var newRequest))
        {
            Request = (RequestFullInfoProviderViewDto)newRequest;
            IsWaitingForAssign = Request.Status == RequestStatus.Sent || Request.Status == RequestStatus.HasAnswers;
            IsWaitingForStart = Request.Status == RequestStatus.Concluded;
            IsWaitingForFinish = Request.Status == RequestStatus.Started;
        }

        if (query.TryGetValue(nameof(Price), out var newPrice))
        {
            Price = (float)newPrice;
        }

        query.Clear();
    }

    [RelayCommand]
    private async Task ViewProviderDetails(ProviderRequestInteractionInfo provider)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderInfoViewModel.ProviderId), provider.ProviderId }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderInfoPage), navigationParameters);
    }

    [RelayCommand]
    private async Task AssignForRequest()
    {
        var response = await _requestService.ServiceProviderAssignForRequestAsync(Request.Id, Price);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceProviderRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Assigning for Request failed", response.Error);
    }

    [RelayCommand]
    private async Task SetNextStatusForRequest()
    {
        var response = await _requestService.SetRequestNextStatusAsync(Request.Id);

        if (response.IsSuccess)
        {
            Request.Status = response.Result.Status;
            await Shell.Current.GoToAsync("//ServiceProviderRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Setting next status for request failed", response.Error);
    }

    [RelayCommand]
    public async Task GoBackToPreviousStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderRequestViewModel.Price), Price }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
