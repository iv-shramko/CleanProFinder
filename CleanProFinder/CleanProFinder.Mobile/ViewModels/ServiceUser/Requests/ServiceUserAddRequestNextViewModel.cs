using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Requests;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;

[QueryProperty(nameof(Request), nameof(Request))]
public partial class ServiceUserAddRequestNextViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IRequestService _requestService;

    public ServiceUserAddRequestNextViewModel(IDialogService dialogService, IRequestService requestService)
    {
        _dialogService = dialogService;
        _requestService = requestService;
    }

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private string _serviceProviderId;

    [ObservableProperty]
    private string _serviceProviderName;

    [ObservableProperty]
    private bool _hasServiceProviderId;

    [ObservableProperty]
    private RequestFullInfoDto _request;

    [RelayCommand]
    private async Task AddRequest()
    {
        var response = await _requestService.AddServiceUserRequestAsync(Request.PremiseId, Request.Services, Description);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Request failed", response.Error);
    }

    [RelayCommand]
    public async Task GoBackToPreviousStep()
    {
        Request.Description = Description;

        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestViewModel.Request), Request }
        };

        await Shell.Current.GoToAsync("..", navigationParameters);
    }
}
