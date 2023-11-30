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
    private readonly IPremiseService _premiseService;

    public ServiceUserSelectPremiseViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
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

        var response = await _premiseService.GetPremisesAsync();

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
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserConfirmPremiseSelectionViewModel.SelectedPremiseId), premise.Id }
        };

        await Shell.Current.GoToAsync($"{nameof(ServiceUserConfirmPremiseSelectionPage)}", navigationParameters);
    }

    [RelayCommand]
    private void Search()
    {

    }
}
