using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
public partial class ServiceUserReadPremiseViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public ServiceUserReadPremiseViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
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
}
