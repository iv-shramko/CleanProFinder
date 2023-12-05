using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace CleanProFinder.Server.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var identity = Context.User!.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            await Groups.AddToGroupAsync(Context.ConnectionId, identity);
            await base.OnConnectedAsync();
        }
    }
}
