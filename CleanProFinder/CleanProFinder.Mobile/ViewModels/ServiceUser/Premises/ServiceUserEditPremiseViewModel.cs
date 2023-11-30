using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
public partial class ServiceUserEditPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public ServiceUserEditPremiseViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
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

        var response = await _premiseService.GetPremiseAsync(payload);

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
            await _premiseService.EditPremiseAsync(Guid.Parse(PremiseId), Square, Description, Address);

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
        var response = await _premiseService.DeletePremiseAsync(PremiseId);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Deleting Premise Failed", response.Error);
    }
}
