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
    private readonly IUserPremiseService _userPremisesService;

    public ServiceUserEditPremiseViewModel(IDialogService dialogService, IUserPremiseService userPremisesServic)
    {
        _dialogService = dialogService;
        _userPremisesService = userPremisesServic;
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
        ServiceResponse<OwnPremiseFullInfoDto> response =
           await _userPremisesService.GetServiceUserPremiseFullInfoAsync(new Dictionary<string, object> { { "premiseId", premiseId } });

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
        float squareAsFloat;

        if (float.TryParse(Square, out float result))
        {
            squareAsFloat = result;
        }
        else
        {
            await _dialogService.ShowAlertAsync("Incorect Square Format", String.Empty, "Ok");
            return;
        }

        ServiceResponse response = 
            await _userPremisesService.EditServiceUserPremiseAsync(Guid.Parse(PremiseId), squareAsFloat, Description, Address);

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
        ServiceResponse response =
           await _userPremisesService.DeleteServiceUserPremiseAsync(new Dictionary<string, object> { { "premiseId", PremiseId } });

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Deleting Premise failed", response.Error);
    }
}
