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

    public ServiceUserEditPremiseViewModel(IDialogService dialogService, IUserPremiseService userPremisesServic)
    {
        _dialogService = dialogService;
        _userPremiseService = userPremisesServic;
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
    private string _square;

    [ObservableProperty]
    private string _description;

    [ObservableProperty]
    private bool _isEditing;

    [RelayCommand]
    private async void StartEditing()
    {
        IsEditing = true;
    }

    private async void LoadPremise(string premiseId)
    {
        Dictionary<string, object> payload = new Dictionary<string, object>
        {
            { "premiseId", premiseId }
        };

        ServiceResponse<OwnPremiseFullInfoDto> response =
           await _userPremiseService.GetServiceUserPremiseAsync(payload);

        if (response.IsSuccess)
        {
            Address = response.Result.Address;
            Square = response.Result.Square.ToString();
            Description = response.Result.Description;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Loading Premise failed", response.Error);
    }

    [RelayCommand]
    private async void EditPremise()
    {
        ServiceResponse response = 
            await _userPremiseService.EditServiceUserPremiseAsync(Guid.Parse(PremiseId), float.Parse(Square), Description, Address);

        if (response.IsSuccess)
        {
            await _dialogService.ShowAlertAsync("Updating Premise succeeded", String.Empty, "Ok");
            IsEditing = false;
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Updating Premise failed", response.Error);
    }

    [RelayCommand]
    private async void DeletePremise()
    {
        Dictionary<string, object> payload = new Dictionary<string, object>
        {
            { "premiseId", PremiseId }
        };

        ServiceResponse response =
           await _userPremiseService.DeleteServiceUserPremiseAsync(payload);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Deleting Premise failed", response.Error);
    }
}
