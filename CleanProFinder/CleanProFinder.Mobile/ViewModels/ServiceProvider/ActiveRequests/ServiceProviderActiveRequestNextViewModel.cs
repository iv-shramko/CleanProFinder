using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

[QueryProperty(nameof(Request), nameof(Request))]
[QueryProperty(nameof(Price), nameof(Price))]
public partial class ServiceProviderActiveRequestNextViewModel : ObservableObject
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
        await Shell.Current.GoToAsync(
            $"..?{nameof(ServiceProviderActiveRequestViewModel.Price)}={Price}");
    }
}
