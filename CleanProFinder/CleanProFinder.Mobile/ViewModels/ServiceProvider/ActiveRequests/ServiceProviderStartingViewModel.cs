using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceProvider.ActiveRequests;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceProvider.ActiveRequests;

public partial class ServiceProviderStartingViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceProviderStartingViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
        IsRefreshing = true;
    }

    [ObservableProperty]
    private ObservableCollection<RequestShortInfoDto> _requests;

    [ObservableProperty]
    private string _requestId;

    [ObservableProperty]
    private string _searchQuery;

    [ObservableProperty]
    private bool _isRefreshing;

    [RelayCommand]
    private async Task LoadRequests()
    {
        IsRefreshing = true;

        var response = await _requestService.GetActiveRequestsAsync();

        if (response.IsSuccess)
        {
            Requests = new ObservableCollection<RequestShortInfoDto>(response.Result);
            IsRefreshing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Requests Failed", response.Error);

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task ViewActiveRequest(RequestShortInfoDto request)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceProviderActiveRequestViewModel.RequestId), request.Id }
        };

        await Shell.Current.GoToAsync(nameof(ServiceProviderActiveRequestPage), navigationParameters);
    }

    [RelayCommand]
    private void Search()
    {

    }
}
