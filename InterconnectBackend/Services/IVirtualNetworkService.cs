using Models.DTO;

namespace Services
{
    public interface IVirtualNetworkService
    {
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualSwitch(int sourceEntityId, int destinationEntityId);
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId);
        Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections();
        Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name);
        Task<List<VirtualSwitchEntityDTO>> GetVisibleVirtualSwitchEntities();
        Task<VirtualSwitchEntityDTO> UpdateVirtualSwitchEntityPosition(int entityId, int x, int y);
        Task<InternetEntityModelDTO> CreateInternet();
        Task<List<InternetEntityModelDTO>> GetInternetEntities();
        Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y);
    }
}
