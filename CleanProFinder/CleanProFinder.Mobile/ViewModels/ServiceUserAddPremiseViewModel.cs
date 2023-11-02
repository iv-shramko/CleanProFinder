using CommunityToolkit.Mvvm.ComponentModel;
using CleanProFinder.Mobile.Services;
using CleanProFinder.Shared.ServiceResponseHandling;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels;

public partial class ServiceUserAddPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IUserPremiseService _userPremiseService;

    public ServiceUserAddPremiseViewModel(IDialogService dialogService, IUserPremiseService userPremiseService)
    {
        _dialogService = dialogService;
        _userPremiseService = userPremiseService;
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
        ServiceResponse response = 
            await _userPremiseService.AddServiceUserPremiseAsync(float.Parse(Square), Description, Address);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Premise failed", response.Error);
    }
}
