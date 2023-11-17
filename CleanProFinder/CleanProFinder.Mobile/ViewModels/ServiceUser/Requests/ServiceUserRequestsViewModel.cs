using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserRequestsViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserRequestService _userRequestService;
    private readonly IPremiseService _userPremiseService;

    public ServiceUserRequestsViewModel(IDialogService dialogService, IUserRequestService userRequestService, IPremiseService userPremiseService)
    {
        _dialogService = dialogService;
        _userRequestService = userRequestService;
        _userPremiseService = userPremiseService;
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

        var response = await _userRequestService.GetServiceUserRequestsAsync();

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
    private async Task EditRequest(RequestShortInfoDto request)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(ServiceUserEditRequestPage)}?{nameof(ServiceUserEditRequestViewModel.RequestId)}={request.Id}");
    }

    [RelayCommand]
    private async Task AddRequest()
    {
        await Shell.Current.GoToAsync(nameof(ServiceUserAddRequestPage));
    }

    [RelayCommand]
    private void Search()
    {

    }
}
