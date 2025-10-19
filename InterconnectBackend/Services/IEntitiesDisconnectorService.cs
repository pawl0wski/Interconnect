using Models.DTO;

namespace Services
{
    public interface IEntitiesDisconnectorService
    {
        Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId);
        Task DisconnectVirtualMachineFromVirtualNetworkNode(int connectionId, int sourceEntityId, int destinationEntityId);
        Task DisconnectVirtualMachineFromVirtualMachine(int connectionId, int sourceEntityId, int destinationEntityId);
        Task DisconnectVirtualMachineFromInternet(int connectionId, int virtualMachineEntityId, int internetEntityId);
    }
}
