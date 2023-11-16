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
    private readonly IUserPremiseService _userPremiseService;

    public ServiceUserPremisesViewModel(IDialogService dialogService, IUserPremiseService userPremisesService)
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

        var response = await _userPremiseService.GetServiceUserPremisesAsync();
        
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
        await Shell.Current.GoToAsync(
            $"{nameof(ServiceUserEditPremisePage)}?{nameof(ServiceUser.Premises.ServiceUserEditPremiseViewModel.PremiseId)}={premise.Id}");
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
