﻿using CleanProFinder.Mobile.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CleanProFinder.Mobile.ViewModels.ServiceUser.Premises;

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
    private float _square;

    [ObservableProperty]
    private string _description;

    [RelayCommand]
    private async Task AddPremise()
    {
        var response = 
            await _userPremiseService.AddServiceUserPremiseAsync(Square, Description, Address);

        if (response.IsSuccess)
        {
            await Shell.Current.GoToAsync("..");
            return;
        }

        await _dialogService.ShowErrorAlertAsync("Adding Premise failed", response.Error);
    }
}