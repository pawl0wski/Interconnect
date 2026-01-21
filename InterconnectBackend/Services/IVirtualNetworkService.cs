using Models;
using Models.Database;
using Models.DTO;

namespace Services
{
    /// <summary>
    /// Service managing virtual networks.
    /// </summary>
    public interface IVirtualNetworkService
    {
        /// <summary>
        /// Retrieves a list of all connections in the virtual network.
        /// </summary>
        /// <returns>List of connections.</returns>
        Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections();
        
        /// <summary>
        /// Creates a new virtual network node.
        /// </summary>
        /// <param name="name">Node name.</param>
        /// <returns>Created network node.</returns>
        Task<VirtualNetworkNodeEntityDTO> CreateVirtualNetworkNode(string? name);
        
        /// <summary>
        /// Retrieves a list of visible virtual network nodes.
        /// </summary>
        /// <returns>List of visible nodes.</returns>
        Task<List<VirtualNetworkNodeEntityDTO>> GetVisibleVirtualNetworkNodeEntities();
        
        /// <summary>
        /// Updates the position of a virtual network node.
        /// </summary>
        /// <param name="entityId">Node identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated node.</returns>
        Task<VirtualNetworkNodeEntityDTO> UpdateVirtualNetworkNodeEntityPosition(int entityId, int x, int y);
        
        /// <summary>
        /// Attaches a network interface to a virtual machine.
        /// </summary>
        /// <param name="vmId">Virtual machine identifier.</param>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="interfaceDefinition">Network interface definition.</param>
        Task AttachNetworkInterfaceToVirtualMachine(int vmId, int connectionId, VirtualNetworkInterfaceCreateDefinition interfaceDefinition);
        
        /// <summary>
        /// Updates the network for a virtual machine network interface.
        /// </summary>
        /// <param name="vmId">Virtual machine identifier.</param>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="networkName">New network name.</param>
        Task UpdateNetworkForVirtualMachineNetworkInterface(int vmId, int connectionId, string networkName);
        
        /// <summary>
        /// Creates a virtual network for Internet connection.
        /// </summary>
        /// <returns>Created virtual network.</returns>
        Task<VirtualNetworkModel> CreateInternetVirtualNetwork();
        
        /// <summary>
        /// Creates a virtual network for a network node.
        /// </summary>
        /// <returns>Created virtual network.</returns>
        Task<VirtualNetworkModel> CreateNodeVirtualNetwork();
        
        /// <summary>
        /// Retrieves all virtual networks.
        /// </summary>
        /// <returns>List of virtual networks.</returns>
        Task<List<VirtualNetworkModel>> GetAllVirtualNetworks();
        
        /// <summary>
        /// Retrieves a virtual network based on bridge name.
        /// </summary>
        /// <param name="bridgeName">Network bridge name.</param>
        /// <returns>Virtual network.</returns>
        Task<VirtualNetworkModel> GetVirtualNetworkUsingBridgeName(string bridgeName);
    }
}
