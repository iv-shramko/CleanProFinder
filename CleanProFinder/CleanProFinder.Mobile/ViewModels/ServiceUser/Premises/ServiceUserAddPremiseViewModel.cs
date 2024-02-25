using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

public partial class ServiceUserAddPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public ServiceUserAddPremiseViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
    }

    [ObservableProperty]
    private string _address;

    [ObservableProperty]
    private float _square;

    [ObservableProperty]
    private string _description;

    [RelayCommand]
    private async Task AddPremise()
    {
        var response = 
            await _premiseService.AddPremiseAsync(Square, Description, Address);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Premise failed", response.Error);
    }
}
