using CleanProFinder.Shared.Errors.Base;

namespace CleanProFinder.Mobile.Services;

public interface IDialogService
{
    Task ShowAlertAsync(string title, string message, string accept);
    Task ShowAlertAsync(string title, string message, string accept, string cancel);
    Task ShowErrorAlertAsync(string title, Error error);
}