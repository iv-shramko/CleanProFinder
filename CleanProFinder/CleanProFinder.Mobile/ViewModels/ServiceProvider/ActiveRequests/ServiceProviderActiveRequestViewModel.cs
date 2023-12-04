using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;
using CleanProFinder.Mobile.ViewModels.Info;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

public partial class ServiceProviderActiveRequestViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderActiveRequestViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private Guid _requestId;

    [ObservableProperty]
    private RequestFullInfoProviderViewDto _request;

    public float Price { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(RequestId), out var requestId))
        {
            LoadRequest((Guid)requestId);
        }
        if (query.TryGetValue(nameof(Request), out var newRequest))
        {
            Request = (RequestFullInfoProviderViewDto)newRequest;
        }
        if (query.TryGetValue(nameof(Price), out var price))
        {
            Price = (float)price;
        }

        query.Clear();
    }

    private async void LoadRequest(Guid requestId)
    {
        var response = await _requestService.ServiceProviderGetRequestAsync(requestId);

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
