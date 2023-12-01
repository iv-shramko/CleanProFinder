using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.Requests;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

public partial class ServiceUserEditRequestViewModel : ObservableObject, IQueryAttributable
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserEditRequestViewModel(
        IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private Guid _requestId;

    [ObservableProperty]
    private RequestFullInfoDto _request;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue(nameof(RequestId), out var requestId))
        {
            LoadRequest((Guid)requestId);
        }

        query.Clear();
    }

    private async void LoadRequest(Guid requestId)
    {
        var response = await _requestService.GetServiceUserRequestAsync(requestId);

        if (response.IsSuccess)
        {
            Request = response.Result;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Request Failed", response.Error);
    }

    [RelayCommand]
    private async Task ViewPremise()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserPremiseInfoViewModel.PremiseId), Request.PremiseId }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserPremiseInfoPage), navigationParameters);
    }
    
    [RelayCommand]
    private async Task NextStep()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserEditRequestNextViewModel.Request), Request },
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserEditRequestNextPage), navigationParameters);
    }
}
