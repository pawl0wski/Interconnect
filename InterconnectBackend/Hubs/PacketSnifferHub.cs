using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    public class PacketSnifferHub : Hub
    {
        public readonly static string DefaultGroupName = "default";

        public async Task JoinDefaultGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, DefaultGroupName);
        }
    }
}
