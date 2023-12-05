using CleanProFinder.Shared.Dto.Notifications;
using CleanProFinder.Shared.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace CleanProFinder.Server.Hubs.Notifiers
{
    public class RequestNotifier
    {
        private readonly IHubContext<NotificationHub> _hubContext;

        public RequestNotifier(IHubContext<NotificationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task RequestStatusChangedAsync(Guid userId, RequestStatusChangeMessage message)
        {
            await _hubContext.Clients.Group(userId.ToString()).SendAsync(NotificationTag.RequestStatusChange, message);
        }

        public async Task ProviderAssignedToRequestAsync(Guid userId, ProviderAssignedToRequestMessage message)
        {
            await _hubContext.Clients.Group(userId.ToString()).SendAsync(NotificationTag.ProviderAssignedToRequest, message);
        }
    }
}
