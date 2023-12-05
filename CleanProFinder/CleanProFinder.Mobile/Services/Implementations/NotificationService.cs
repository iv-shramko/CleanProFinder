using CleanProFinder.Mobile.Services.Interfaces;
using CleanProFinder.Mobile.ViewModels.ServiceUser.Requests;
using CleanProFinder.Mobile.Views.ServiceUser.Requests;
using CleanProFinder.Shared.Dto.Notifications;
using CleanProFinder.Shared.Helpers;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Plugin.LocalNotification;
using Plugin.LocalNotification.EventArgs;

namespace CleanProFinder.Mobile.Services.Implementations;

public class NotificationService : Interfaces.INotificationService
{
    private readonly HubConnection _hubConnection;
    private readonly IRequestService _requestService;

    public NotificationService(IConfiguration configuration, IRequestService requestService)
    {
        _hubConnection = new HubConnectionBuilder()
            .WithAutomaticReconnect()
            .WithUrl(configuration["BaseUrl"] + "notifications", options =>
                {
                    options.AccessTokenProvider = () => SecureStorage.GetAsync("BearerToken");
                })
            .Build();

        _requestService = requestService;

        LocalNotificationCenter.Current.NotificationActionTapped += OnNotificationActionTapped;
    }

    public async Task Initialize()
    {
        await _hubConnection.StartAsync();

        if (_hubConnection.State == HubConnectionState.Connected)
        {
            const string requestStatusChangedTag = NotificationTag.RequestStatusChange;
            _hubConnection.On<RequestStatusChangeMessage>(requestStatusChangedTag, HandleRequestStatusChanged);

            const string providerAssignedToRequestTag = NotificationTag.ProviderAssignedToRequest;
            _hubConnection.On<ProviderAssignedToRequestMessage>(providerAssignedToRequestTag, HandleProviderAssignedToRequest);
        }
    }

    private void HandleRequestStatusChanged(RequestStatusChangeMessage message)
    {
        var request = new NotificationRequest
        {
            NotificationId = 1,
            Title = "Your request status has changed!",
            Description = $"Request status for {message.RequestPremiseAddress} changed to {message.NewStatus}."
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private void HandleProviderAssignedToRequest(ProviderAssignedToRequestMessage message)
    {
        var request = new NotificationRequest
        {
            NotificationId = 2,
            Title = "Your request has new answers!",
            Description = $"{message.ProviderName} wants to take your {message.RequestPremiseAddress} request.",
            ReturningData = message.RequestId.ToString()
        };

        LocalNotificationCenter.Current.Show(request);
    }

    private async void OnNotificationActionTapped(NotificationActionEventArgs e)
    {
        switch (e.Request.NotificationId)
        {
            case 1: // RequestStatusChanged
                await Shell.Current.GoToAsync("//ServiceUserRequestsPage");
                break;
            case 2: // ProviderAssignedToRequest
                var requestId = Guid.Parse(e.Request.ReturningData);
                var response = await _requestService.GetOwnRequestAsync(requestId);
                var navigationParameters = new Dictionary<string, object>
                {
                    { nameof(ServiceUserEditRequestNextViewModel.Request), response.Result }
                };
                await Shell.Current.GoToAsync(nameof(ServiceUserEditRequestNextPage), navigationParameters);
                break;
        }
    }
}