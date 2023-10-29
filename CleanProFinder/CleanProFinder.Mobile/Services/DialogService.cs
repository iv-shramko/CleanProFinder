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
}