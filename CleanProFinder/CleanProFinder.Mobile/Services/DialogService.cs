using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Mobile.Services;

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