using Models.DTO;
using Models.Enums;

namespace Services
{
    public interface IEntitiesConnectorService
    {
        Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntitiyId, EntityType destinationEntityType);
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualSwitch(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceEntityId, int destinationEntityId);
    }
}
