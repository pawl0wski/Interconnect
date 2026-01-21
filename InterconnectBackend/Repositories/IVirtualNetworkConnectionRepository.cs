using Models.Database;
using Models.Enums;

namespace Repositories
{
    /// <summary>
    /// Repository managing entity connections in the virtual network.
    /// </summary>
    public interface IVirtualNetworkConnectionRepository
    {
        /// <summary>
        /// Retrieves connections associated with a specific entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="type">Entity type.</param>
        /// <returns>List of connections.</returns>
        public Task<List<VirtualNetworkEntityConnectionModel>> GetUsingEntityId(int id, EntityType type);
        
        /// <summary>
        /// Retrieves a connection by identifier.
        /// </summary>
        /// <param name="id">Connection identifier.</param>
        /// <returns>Connection.</returns>
        public Task<VirtualNetworkEntityConnectionModel> GetById(int id);
        
        /// <summary>
        /// Creates a new connection between two entities.
        /// </summary>
        /// <param name="firstEntityId">First entity identifier.</param>
        /// <param name="firstEntityType">First entity type.</param>
        /// <param name="secondEntityId">Second entity identifier.</param>
        /// <param name="secondEntityType">Second entity type.</param>
        /// <returns>Created connection.</returns>
        public Task<VirtualNetworkEntityConnectionModel> Create(int firstEntityId, EntityType firstEntityType, int secondEntityId, EntityType secondEntityType);
        
        /// <summary>
        /// Retrieves all connections.
        /// </summary>
        /// <returns>List of connections.</returns>
        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll();
        
        /// <summary>
        /// Removes a connection.
        /// </summary>
        /// <param name="id">Connection identifier.</param>
        public Task Remove(int id);
    }
}
