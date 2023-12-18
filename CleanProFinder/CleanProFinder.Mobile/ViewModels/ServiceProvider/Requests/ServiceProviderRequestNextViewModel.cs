using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.Requests;

[QueryProperty(nameof(Request), nameof(Request))]
[QueryProperty(nameof(Price), nameof(Price))]
[QueryProperty(nameof(IsConcluded), nameof(IsConcluded))]
public partial class ServiceProviderRequestNextViewModel : ObservableObject
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
    private bool _isConcluded;

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
    public async Task GoBackToPreviousStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderRequestViewModel.Price), Price }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
