using Models.DTO;
using Models.Enums;

namespace Services
{
    public interface IVirtualNetworkService
    {
        Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntitiyId, EntityType destinationEntityType);
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualSwitch(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId);
        Task DisconnectVirtualMachineToVirtualSwitch(int connectionId, int sourceEntityId, int destinationEntityId);
        Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections();
        Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name);
        Task<List<VirtualSwitchEntityDTO>> GetVisibleVirtualSwitchEntities();
        Task<VirtualSwitchEntityDTO> UpdateVirtualSwitchEntityPosition(int entityId, int x, int y);
        Task<InternetEntityModelDTO> CreateInternet();
        Task<List<InternetEntityModelDTO>> GetInternetEntities();
        Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y);
    }
}
