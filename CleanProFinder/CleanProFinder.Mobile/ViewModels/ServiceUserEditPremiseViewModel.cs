using CommunityToolkit.Mvvm.ComponentModel;
using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.Input;
using CleanProFinder.Shared.Dto.Premises;

namespace CleanProFinder.Mobile.ViewModels;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
public partial class ServiceUserEditPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserPremiseService _userPremiseService;

    public ServiceUserEditPremiseViewModel(IDialogService dialogService, IUserPremiseService userPremisesService)
    {
        _dialogService = dialogService;
        _userPremiseService = userPremisesService;
        IsEditing = false;
    }

    private string _premiseId;

    public string PremiseId
    {
        get => _premiseId;
        set
        {
            SetProperty(ref _premiseId, value);
            LoadPremise(value);
        }
    }

    [ObservableProperty]
    private string _address;

    [ObservableProperty]
    private float _square;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private bool _isEditing;

    private async void LoadPremise(string premiseId)
    {
        var payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        var response = await _userPremiseService.GetServiceUserPremiseAsync(payload);

        if (response.IsSuccess)
        {
            Address = response.Result.Address;
            Square = response.Result.Square;
            Description = response.Result.Description;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise Failed", response.Error);
    }

    [RelayCommand]
    private void StartEditing()
    {
        IsEditing = true;
    }

    [RelayCommand]
    private async Task EditPremise()
    {
        var response =
            await _userPremiseService.EditServiceUserPremiseAsync(Guid.Parse(PremiseId), Square, Description, Address);

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Updating Premise Succeeded",
                "You have successfully updated your premise.", "Ok");
            IsEditing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Updating Premise Failed", response.Error);
    }

    [RelayCommand]
    private async Task DeletePremise()
    {
        var payload = new Dictionary<string, object>
        {
            { "premiseId", PremiseId }
        };

        var response = await _userPremiseService.DeleteServiceUserPremiseAsync(payload);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Deleting Premise Failed", response.Error);
    }
}
