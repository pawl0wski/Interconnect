using Microsoft.AspNetCore.SignalR;
using Models.Responses;

namespace Hubs
{
    public class ConnectionStatusHub : Hub
    {
        public PingResponse Ping()
        {
            return PingResponse.WithSuccess("Pong");
        }
    }
}
