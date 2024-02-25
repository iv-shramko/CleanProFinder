using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CleanProFinder.Mobile.ViewModels.Info;

[QueryProperty(nameof(PremiseId), nameof(PremiseId))]
public partial class PremiseInfoViewModel : ObservableObject
{
    private readonly IDialogService _dialogService;
    private readonly IPremiseService _premiseService;

    public PremiseInfoViewModel(IDialogService dialogService, IPremiseService premiseService)
    {
        _dialogService = dialogService;
        _premiseService = premiseService;
    }

    private Guid _premiseId;

    public Guid PremiseId
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

    private async void LoadPremise(Guid premiseId)
    {
        var response = await _premiseService.GetPremiseAsync(premiseId);

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
