using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing virtual networks in the database.
    /// </summary>
    public interface IVirtualNetworkRepository
    {
        /// <summary>
        /// Creates a new virtual network.
        /// </summary>
        /// <param name="bridgeName">Network bridge name.</param>
        /// <param name="uuid">Network UUID.</param>
        /// <param name="ipAddress">Network IP address (optional).</param>
        /// <returns>Created virtual network.</returns>
        public Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid, string? ipAddress = null);
        
        /// <summary>
        /// Retrieves a virtual network by identifier.
        /// </summary>
        /// <param name="id">Network identifier.</param>
        /// <returns>Virtual network.</returns>
        public Task<VirtualNetworkModel> GetById(int id);
        
        /// <summary>
        /// Retrieves a virtual network by UUID.
        /// </summary>
        /// <param name="uuid">Network UUID.</param>
        /// <returns>Virtual network.</returns>
        public Task<VirtualNetworkModel> GetByUuid(Guid uuid);
        
        /// <summary>
        /// Retrieves a virtual network with nodes by UUID.
        /// </summary>
        /// <param name="uuid">Network UUID.</param>
        /// <returns>Virtual network with nodes.</returns>
        public Task<VirtualNetworkModel> GetByUuidWithVirtualNetworkNodes(Guid uuid);
        
        /// <summary>
        /// Removes a virtual network.
        /// </summary>
        /// <param name="id">Network identifier.</param>
        public Task Remove(int id);
        
        /// <summary>
        /// Retrieves all virtual networks.
        /// </summary>
        /// <returns>List of virtual networks.</returns>
        public Task<List<VirtualNetworkModel>> GetAll();
        
        /// <summary>
        /// Retrieves a virtual network by bridge name.
        /// </summary>
        /// <param name="bridgeName">Network bridge name.</param>
        /// <returns>Virtual network.</returns>
        public Task<VirtualNetworkModel> GetByBridgeName(string bridgeName);
    }
}
