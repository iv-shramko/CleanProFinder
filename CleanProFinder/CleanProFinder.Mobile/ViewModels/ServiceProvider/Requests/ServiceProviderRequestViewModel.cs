using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceProvider.Requests;
using CleanProFinder.Mobile.ViewModels.Info;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Enums;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

public partial class ServiceProviderRequestViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderRequestViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private Guid _requestId;

    [ObservableProperty]
    private RequestFullInfoProviderViewDto _request;

    public float Price { get; set; }

    public bool IsConcluded { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(RequestId), out var requestId))
        {
            LoadRequest((Guid)requestId);
        }

        if (query.TryGetValue(nameof(Price), out var price))
        {
            Price = (float)price;
        }

        query.Clear();
    }

    private async void LoadRequest(Guid requestId)
    {
        var response = await _requestService.GetRequestAsync(requestId);

        if (response.IsSuccess)
        {
            Request = response.Result;
            IsConcluded = Request.Status == RequestStatus.Concluded;
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
            { nameof(ServiceProviderRequestNextViewModel.Request), Request },
            { nameof(ServiceProviderRequestNextViewModel.Price), Price },
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderRequestNextPage), navigationParameters);
    }
}
