using Microsoft.AspNetCore.SignalR;

namespace Hubs
{
    /// <summary>
    /// SignalR hub for transmitting captured network packets.
    /// </summary>
    public class PacketSnifferHub : Hub
    {
        /// <summary>
        /// Default group name for packet analyzer.
        /// </summary>
        public readonly static string DefaultGroupName = "default";

        /// <summary>
        /// Adds client to default packet analyzer group.
        /// </summary>
        public async Task JoinDefaultGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, DefaultGroupName);
        }
    }
}
