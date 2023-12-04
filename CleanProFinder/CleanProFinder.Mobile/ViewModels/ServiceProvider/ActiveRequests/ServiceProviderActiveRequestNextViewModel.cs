using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

public partial class ServiceProviderActiveRequestNextViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderActiveRequestNextViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private RequestFullInfoProviderViewDto _request;

    [ObservableProperty]
    private float _price;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
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
            await Shell.Current.GoToAsync("//ServiceProviderStartingPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Assigning for Request failed", response.Error);
    }

    [RelayCommand]
    public async Task GoBackToPreviousStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderActiveRequestViewModel.Request), Request },
            { nameof(ServiceProviderActiveRequestViewModel.Price), Price }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
