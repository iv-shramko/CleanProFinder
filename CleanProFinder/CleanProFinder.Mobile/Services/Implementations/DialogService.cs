using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Mobile.Services.Implementations;

public class DialogService : IDialogService
{
    public Task ShowAlertAsync(string title, string message, string accept)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept);
    }

    public Task ShowAlertAsync(string title, string message, string accept, string cancel)
    {
        return Application.Current.MainPage.DisplayAlert(title, message, accept, cancel);
    }

    public async Task ShowErrorAlertAsync(string title, Error error)
    {
        if (error == null || !(error.ServiceErrors.Any() || error.ValidationErrors.Any()))
        {
            var serviceError = new ServiceError
            {
                ErrorMessage = "An unknown error has occurred."
            };
            error = new Error(serviceErrors: new List<ServiceError>() { serviceError });
        }

        var errorText = "";
        
        foreach (var serviceError in error.ServiceErrors)
        {
            errorText += serviceError.ErrorMessage + "\n";
        }

        foreach (var validationError in error.ValidationErrors)
        {
            errorText += validationError.ErrorMessage + "\n";
        }
        
        await ShowAlertAsync(title, errorText, "OK");
    }
}