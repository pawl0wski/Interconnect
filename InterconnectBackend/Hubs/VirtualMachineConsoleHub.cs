using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Requests;
using Models.Responses;
using Services;

namespace Hubs
{
    public class VirtualMachineConsoleHub : Hub
    {
        private readonly IVirtualMachineConsoleService _virtualMachineConsole;

        public VirtualMachineConsoleHub(IVirtualMachineConsoleService virtualMachineConsole)
        {
            _virtualMachineConsole = virtualMachineConsole;
        }

        public async Task JoinConsoleGroup(string uuid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, uuid);
        }

        public async Task LeaveConsoleGroup(string uuid)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, uuid);
        }

        public TerminalDataResponse GetInitialDataForConsole(string uuid)
        {
            var data = _virtualMachineConsole.GetInitialConsoleData(Guid.Parse(uuid));

            return TerminalDataResponse.WithSuccess(new TerminalData
            {
                Uuid = uuid,
                Data = Convert.ToBase64String(data.ToArray())
            });
        }

        public void SendDataToConsole(SendDataToConsoleRequest request)
        {
            _virtualMachineConsole.SendBytesToVirtualMachineConsoleByUuid(Guid.Parse(request.Uuid), request.Data);
        }
    }
}
