using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing Internet entities in the database.
    /// </summary>
    public interface IInternetEntityRepository
    {
        /// <summary>
        /// Creates a new Internet entity.
        /// </summary>
        /// <param name="virtualNetwork">Virtual network to connect.</param>
        /// <returns>Created Internet entity.</returns>
        public Task<InternetEntityModel> Create(VirtualNetworkModel virtualNetwork);
        
        /// <summary>
        /// Retrieves all Internet entities.
        /// </summary>
        /// <returns>List of Internet entities.</returns>
        public Task<List<InternetEntityModel>> GetAll();
        
        /// <summary>
        /// Retrieves an Internet entity by identifier.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Internet entity.</returns>
        public Task<InternetEntityModel> GetById(int id);
        
        /// <summary>
        /// Updates the position of an Internet entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated entity.</returns>
        public Task<InternetEntityModel> UpdatePosition(int id, int x, int y);

        /// <summary>
        /// Deletes the entity with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        public Task Remove(int id);
    }
}
