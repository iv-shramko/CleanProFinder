using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Dto.Premises;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using System.Collections.ObjectModel;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

public partial class ServiceUserSelectPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _userPremiseService;

    public ServiceUserSelectPremiseViewModel(IDialogService dialogService, IPremiseService userPremisesService)
    {
        _dialogService = dialogService;
        _userPremiseService = userPremisesService;
        IsRefreshing = true;
    }

    [ObservableProperty]
    private ObservableCollection<OwnPremiseShortInfoDto> _premises;

    [ObservableProperty]
    private string _premiseId;

    [ObservableProperty]
    private string _searchQuery;

    [ObservableProperty]
    private bool _isRefreshing;

    [RelayCommand]
    private async Task LoadPremises()
    {
        IsRefreshing = true;

        var response = await _userPremiseService.GetPremisesAsync();

        if (response.IsSuccess)
        {
            Premises = new ObservableCollection<OwnPremiseShortInfoDto>(response.Result);
            IsRefreshing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premises Failed", response.Error);

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task SelectPremise(OwnPremiseShortInfoDto premise)
    {
        await Shell.Current.GoToAsync(
            $"{nameof(ServiceUserConfirmPremiseSelectionPage)}?{nameof(ServiceUserAddRequestViewModel.PremiseId)}={premise.Id}");
    }

    [RelayCommand]
    private void Search()
    {

    }
}
