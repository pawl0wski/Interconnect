using Microsoft.AspNetCore.SignalR;
using Models.Responses;

namespace Hubs
{
    public class ConnectionStatusHub : Hub
    {
        public StringResponse Ping()
        {
            return StringResponse.WithSuccess("Pong");
        }
    }
}
