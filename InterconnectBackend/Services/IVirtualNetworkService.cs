using Models;
using Models.Database;
using Models.DTO;

namespace Services
{
    public interface IVirtualNetworkService
    {
        Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections();
        Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name);
        Task<List<VirtualSwitchEntityDTO>> GetVisibleVirtualSwitchEntities();
        Task<VirtualSwitchEntityDTO> UpdateVirtualSwitchEntityPosition(int entityId, int x, int y);
        Task AttachNetworkInterfaceToVirtualMachine(int id, VirtualNetworkInterfaceCreateDefinition interfaceDefinition);
        Task UpdateNetworkForVirtualMachineNetworkInterface(int id, string networkName);
        Task<VirtualNetworkModel> CreateInternetVirtualNetwork();
        Task<VirtualNetworkModel> CreateSwitchVirtualNetwork();
        Task<List<VirtualNetworkModel>> GetAllVirtualNetworks();
        Task<VirtualNetworkModel> GetVirtualNetworkUsingBridgeName(string bridgeName);
    }
}
