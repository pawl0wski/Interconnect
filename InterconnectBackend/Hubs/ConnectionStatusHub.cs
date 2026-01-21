using Microsoft.AspNetCore.SignalR;
using Models.Responses;

namespace Hubs
{
    /// <summary>
    /// SignalR hub for checking connection status.
    /// </summary>
    public class ConnectionStatusHub : Hub
    {
        /// <summary>
        /// Checks connection with the server.
        /// </summary>
        /// <returns>Response confirming connection.</returns>
        public StringResponse Ping()
        {
            return StringResponse.WithSuccess("Pong");
        }
    }
}
