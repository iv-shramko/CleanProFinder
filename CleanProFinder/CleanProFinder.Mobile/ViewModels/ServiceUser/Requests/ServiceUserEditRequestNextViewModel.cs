using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
[QueryProperty(nameof(ServiceProviderId), nameof(ServiceProviderId))]
[QueryProperty(nameof(Description), nameof(Description))]
[QueryProperty(nameof(RequestId), nameof(RequestId))]
public partial class ServiceUserEditRequestNextViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserRequestService _userRequestService;

    public ServiceUserEditRequestNextViewModel(
        IDialogService dialogService, IUserRequestService userRequestsService)
    {
        _dialogService = dialogService;
        _userRequestService = userRequestsService;
    }

    [ObservableProperty]
    private string _premiseId;

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

    [RelayCommand]
    private async Task CancelRequest()
    {
        var response =
            await _userRequestService.CancelServiceUserRequestAsync(RequestId);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("../..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Canceling Request failed", response.Error);
    }

    [RelayCommand]
    private async Task LastStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            {"PremiseId", PremiseId },
            {"Description", Description }
        };

        await Shell.Current.GoToAsync($"..", navigationParameters);
    }
}
