using Microsoft.AspNetCore.SignalR;
using Models;
using Models.Requests;
using Models.Responses;
using Services;

namespace Hubs
{
    /// <summary>
    /// SignalR hub for communicating with virtual machine console.
    /// </summary>
    public class VirtualMachineConsoleHub : Hub
    {
        private readonly IVirtualMachineConsoleService _virtualMachineConsole;

        public VirtualMachineConsoleHub(IVirtualMachineConsoleService virtualMachineConsole)
        {
            _virtualMachineConsole = virtualMachineConsole;
        }

        /// <summary>
        /// Adds client to console group for a specific virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        public async Task JoinConsoleGroup(string uuid)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, uuid);
        }

        /// <summary>
        /// Removes client from console group for a specific virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        public async Task LeaveConsoleGroup(string uuid)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, uuid);
        }

        /// <summary>
        /// Retrieves initial console data for a virtual machine.
        /// </summary>
        /// <param name="uuid">Virtual machine UUID.</param>
        /// <returns>Initial console data.</returns>
        public TerminalDataResponse GetInitialDataForConsole(string uuid)
        {
            var data = _virtualMachineConsole.GetInitialConsoleData(Guid.Parse(uuid));

            return TerminalDataResponse.WithSuccess(new TerminalData
            {
                Uuid = uuid,
                Data = Convert.ToBase64String(data.ToArray())
            });
        }

        /// <summary>
        /// Sends data to a virtual machine console.
        /// </summary>
        /// <param name="request">Data to send.</param>
        public void SendDataToConsole(SendDataToConsoleRequest request)
        {
            _virtualMachineConsole.SendBytesToVirtualMachineConsoleByUuid(Guid.Parse(request.Uuid), request.Data);
        }
    }
}
