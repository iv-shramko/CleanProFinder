using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.Premises;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

[QueryProperty(nameof(SelectedPremiseId), nameof(SelectedPremiseId))]
public partial class ServiceUserConfirmPremiseSelectionViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public ServiceUserConfirmPremiseSelectionViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
    }

    private Guid _selectedSelectedPremiseId;

    public Guid SelectedPremiseId
    {
        get => _selectedSelectedPremiseId;
        set
        {
            SetProperty(ref _selectedSelectedPremiseId, value);
            LoadPremise(value);
        }
    }

    [ObservableProperty]
    private OwnPremiseFullInfoDto _selectedPremise;

    private async void LoadPremise(Guid premiseId)
    {
        var response = await _premiseService.GetPremiseAsync(premiseId);

        if (response.IsSuccess)
        {
            SelectedPremise = response.Result;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise Failed", response.Error);
    }

    [RelayCommand]
    private async Task ConfirmPremiseSelection()
    {
        var navigationParameters = new Dictionary<string, object>
        {
            { nameof(ServiceUserAddRequestViewModel.SelectedPremise), SelectedPremise }
        };

        await Shell.Current.GoToAsync($"../..", navigationParameters);
    }
}
