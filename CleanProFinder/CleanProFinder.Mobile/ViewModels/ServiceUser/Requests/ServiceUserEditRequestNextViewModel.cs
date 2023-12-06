using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.Info;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Providers;
using CleanProFinder.Mobile.Views.Info;
using CleanProFinder.Mobile.Views.ServiceUser.Providers;
using CleanProFinder.Shared.Dto.Requests;
using CleanProFinder.Shared.Enums;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserEditRequestNextViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserEditRequestNextViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private RequestFullInfoDto _request;

    [ObservableProperty]
    private ProviderRequestInteractionInfo _selectedServiceProvider;

    [ObservableProperty]
    private bool _isCanceled;

    [ObservableProperty]
    private bool _isConcluded;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(Request), out var newRequest))
        {
            Request = (RequestFullInfoDto)newRequest;
            IsCanceled = Request.Status == RequestStatus.Canceled;
            IsConcluded = Request.Status == RequestStatus.Concluded;
            SelectedServiceProvider =
                Request.ProvidersInteractions.FirstOrDefault(i =>
                    i.InteractionStatus == RequestInteractionStatus.Accepted);
        }

        if (query.TryGetValue(nameof(SelectedServiceProvider), out var newServiceProvider))
        {
            SelectedServiceProvider = (ProviderRequestInteractionInfo)newServiceProvider;
        }

        query.Clear();
    }

    [RelayCommand]
    private async Task SelectServiceProvider()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserSelectProviderOfferViewModel.ProviderRequestInteractions), Request.ProvidersInteractions }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserSelectProviderOfferPage), navigationParameters);
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
    private async Task SaveChanges()
    {
        var response = await _requestService.AcceptProviderForRequestAsync(SelectedServiceProvider.ProviderId, Request.Id);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Saving Changes failed", response.Error);
    }

    [RelayCommand]
    private async Task CancelRequest()
    {
        var response = await _requestService.CancelRequestAsync(Request.Id);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Canceling Request failed", response.Error);
    }

    [RelayCommand]
    private static async Task GoBackToRequests()
    {
        await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
    }
}
