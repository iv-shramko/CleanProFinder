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
public partial class ServiceUserAddRequestNextViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserRequestService _userRequestService;

    public ServiceUserAddRequestNextViewModel(
        IDialogService dialogService, IUserRequestService userRequestsService)
    {
        _dialogService = dialogService;
        _userRequestService = userRequestsService;
    }

    [ObservableProperty]
    private string _premiseId;

    private readonly List<Guid> servicesId = new List<Guid> { Guid.Parse("cd2f9ee0-c0ed-4b9b-92ce-0e37e786ec73") };

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
        var response =
            await _userRequestService.AddServiceUserRequestAsync(Guid.Parse(PremiseId), servicesId, Description, null);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("../..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Request failed", response.Error);
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
