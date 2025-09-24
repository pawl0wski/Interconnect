using Models.DTO;

namespace Services
{
    public interface IEntitiesDisconnectorService
    {
        Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId);
        Task DisconnectVirtualMachineFromVirtualSwitch(int connectionId, int sourceEntityId, int destinationEntityId);
        Task DisconnectVirtualMachineFromVirtualMachine(int connectionId, int sourceEntityId, int destinationEntityId);
        Task DisconnectVirtualSwitchFromVirtualSwitch(int connectionId, int sourceEntityId, int destinationEntityId);
    }
}
