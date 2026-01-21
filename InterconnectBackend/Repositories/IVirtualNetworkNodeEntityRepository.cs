using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing virtual network nodes in the database.
    /// </summary>
    public interface IVirtualNetworkNodeEntityRepository
    {
        /// <summary>
        /// Creates a new visible virtual network node.
        /// </summary>
        /// <param name="name">Node name.</param>
        /// <param name="virtualNetwork">Virtual network.</param>
        /// <returns>Created node.</returns>
        public Task<VirtualNetworkNodeEntityModel> Create(string name, VirtualNetworkModel virtualNetwork);
        
        /// <summary>
        /// Creates a new invisible virtual network node.
        /// </summary>
        /// <param name="virtualNetwork">Virtual network.</param>
        /// <returns>Created node.</returns>
        public Task<VirtualNetworkNodeEntityModel> CreateInvisible(VirtualNetworkModel virtualNetwork);
        
        /// <summary>
        /// Retrieves all virtual network nodes.
        /// </summary>
        /// <returns>List of nodes.</returns>
        public Task<List<VirtualNetworkNodeEntityModel>> GetAll();
        
        /// <summary>
        /// Retrieves visible virtual network nodes.
        /// </summary>
        /// <returns>List of visible nodes.</returns>
        public Task<List<VirtualNetworkNodeEntityModel>> GetVisible();
        
        /// <summary>
        /// Retrieves a virtual network node by identifier.
        /// </summary>
        /// <param name="id">Node identifier.</param>
        /// <returns>Network node.</returns>
        public Task<VirtualNetworkNodeEntityModel> GetById(int id);
        
        /// <summary>
        /// Updates the position of a virtual network node.
        /// </summary>
        /// <param name="id">Node identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated node.</returns>
        public Task<VirtualNetworkNodeEntityModel> UpdateEntityPosition(int id, int x, int y);
        
        /// <summary>
        /// Updates the virtual network for a node.
        /// </summary>
        /// <param name="id">Node identifier.</param>
        /// <param name="virtualNetwork">New virtual network.</param>
        /// <returns>Updated node.</returns>
        public Task<VirtualNetworkNodeEntityModel> UpdateNetwork(int id, VirtualNetworkModel virtualNetwork);
        
        /// <summary>
        /// Removes a virtual network node.
        /// </summary>
        /// <param name="id">Node identifier.</param>
        public Task Remove(int id);
    }
}
