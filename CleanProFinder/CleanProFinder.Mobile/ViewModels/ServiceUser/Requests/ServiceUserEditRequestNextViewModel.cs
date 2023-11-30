using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
[QueryProperty(nameof(Description), nameof(Description))]
[QueryProperty(nameof(RequestId), nameof(RequestId))]
[QueryProperty(nameof(IsCanceled), nameof(IsCanceled))]
public partial class ServiceUserEditRequestNextViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserEditRequestNextViewModel(
        IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private string _requestId;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _serviceProviderId;

    [ObservableProperty]
    private string _serviceProviderName;

    [ObservableProperty]
    private bool _hasServiceProviderId;

    [ObservableProperty]
    private bool _isCanceled;

    [RelayCommand]
    private async Task CancelRequest()
    {
        var response =
            await _requestService.CancelServiceUserRequestAsync(RequestId);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Canceling Request failed", response.Error);
    }

    [RelayCommand]
    private async Task GoBack()
    {
        await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
    }

    [RelayCommand]
    private async Task LastStep()
    {
        await Shell.Current.GoToAsync("..");
    }
}
