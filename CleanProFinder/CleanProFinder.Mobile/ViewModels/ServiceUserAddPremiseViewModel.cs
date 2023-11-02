using CommunityToolkit.Mvvm.ComponentModel;
using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceUserAddPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserPremiseService _userPremisesService;

    public ServiceUserAddPremiseViewModel(IDialogService dialogService, IUserPremiseService userPremisesServic)
    {
        _dialogService = dialogService;
        _userPremisesService = userPremisesServic;
    }

    [ObservableProperty]
    private string _address;

    [ObservableProperty]
    private string _square;

    [ObservableProperty]
    private string _description;

    [RelayCommand]
    private async void AddPremise()
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
            await _userPremisesService.AddServiceUserPremiseAsync(squareAsFloat, Description, Address);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }
        await _dialogService.ShowErrorAlertAsync("Adding Premise failed", response.Error);
    }
}
