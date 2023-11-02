using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CleanProFinder.Mobile.Views;
using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.ServiceResponseHandling;
using CleanProFinder.Shared.Dto.Premises;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceUserPremiseListViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserPremiseService _userPremisesService;

    public ServiceUserPremiseListViewModel(IDialogService dialogService, IUserPremiseService userPremisesServic)
    {
        _dialogService = dialogService;
        _userPremisesService = userPremisesServic;
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
    private async void LoadPremises()
    {
        IsRefreshing = true;

        ServiceResponse<IEnumerable<OwnPremiseShortInfoDto>> response =
            await _userPremisesService.GetServiceUserPremiseListAsync();
        
        if (response.IsSuccess)
        {
            Premises = new ObservableCollection<OwnPremiseShortInfoDto>(response.Result);
            IsRefreshing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premises failed", response.Error);

        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task EditPremise(OwnPremiseShortInfoDto premise)
    {
        await Shell.Current.GoToAsync($"{nameof(ServiceUserEditPremisePage)}?{nameof(ServiceUserEditPremiseViewModel.PremiseId)}={premise.Id}");
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
