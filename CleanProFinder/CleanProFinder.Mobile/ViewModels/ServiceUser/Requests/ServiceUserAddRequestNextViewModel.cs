using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
public partial class ServiceUserAddRequestNextViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;
    private readonly IRequestStorage _requestStorage;

    public ServiceUserAddRequestNextViewModel(
        IDialogService dialogService, IRequestService requestService, IRequestStorage requestStorage)
    {
        _dialogService = dialogService;
        _requestService = requestService;
        _requestStorage = requestStorage;

        _description = _requestStorage.Description;
        _serviceProviderId = _requestStorage.ServiceProviderId;
    }

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _serviceProviderId;

    [ObservableProperty]
    private string _serviceProviderName;

    [ObservableProperty]
    private bool _hasServiceProviderId;

    [RelayCommand]
    private async Task AddRequest()
    {
        var response = await _requestService.AddServiceUserRequestAsync(Request.PremiseId, Request.Services, Description);

        if (response.IsSuccess)
        {
            _requestStorage.Reset();

            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Request failed", response.Error);
    }

    [RelayCommand]
    private async Task LastStep()
    {
        _requestStorage.Description = Description;
        _requestStorage.ServiceProviderId = ServiceProviderId;

        await Shell.Current.GoToAsync(nameof(ServiceUserAddRequestPage));
    }
}
