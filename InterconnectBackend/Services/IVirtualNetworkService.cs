using Models;
using Models.Database;
using Models.DTO;

namespace Services
{
    public interface IVirtualNetworkService
    {
        Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections();
        Task<VirtualNetworkNodeEntityDTO> CreateVirtualNetworkNode(string? name);
        Task<List<VirtualNetworkNodeEntityDTO>> GetVisibleVirtualNetworkNodeEntities();
        Task<VirtualNetworkNodeEntityDTO> UpdateVirtualNetworkNodeEntityPosition(int entityId, int x, int y);
        Task AttachNetworkInterfaceToVirtualMachine(int id, VirtualNetworkInterfaceCreateDefinition interfaceDefinition);
        Task UpdateNetworkForVirtualMachineNetworkInterface(int id, string networkName);
        Task<VirtualNetworkModel> CreateInternetVirtualNetwork();
        Task<VirtualNetworkModel> CreateNodeVirtualNetwork();
        Task<List<VirtualNetworkModel>> GetAllVirtualNetworks();
        Task<VirtualNetworkModel> GetVirtualNetworkUsingBridgeName(string bridgeName);
    }
}
