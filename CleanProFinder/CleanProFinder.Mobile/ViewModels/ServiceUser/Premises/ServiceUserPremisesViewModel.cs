using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Views.ServiceUser.Premises;
using CleanProFinder.Shared.Dto.Premises;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

public partial class ServiceUserPremisesViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public ServiceUserPremisesViewModel(IDialogService dialogService, IPremiseService premiseService)
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

        var response = await _premiseService.GetOwnPremisesAsync();
        
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
    private async Task EditPremise(OwnPremiseShortInfoDto premise)
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserEditPremiseViewModel.PremiseId), premise.Id }
        };

        await Shell.Current.GoToAsync(nameof(ServiceUserEditPremisePage), navigationParameters);
    }

    [RelayCommand]
    private async Task AddPremise()
    {
        await Shell.Current.GoToAsync(nameof(ServiceUserAddPremisePage));
    }

    [RelayCommand]
    private void Search()
    {

    }
}
