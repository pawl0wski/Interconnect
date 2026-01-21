using Models.DTO;

namespace Services
{
    /// <summary>
    /// Service managing Internet entities.
    /// </summary>
    public interface IInternetEntityService
    {
        /// <summary>
        /// Creates a new Internet entity.
        /// </summary>
        /// <returns>Created Internet entity.</returns>
        Task<InternetEntityModelDTO> CreateInternet();
        
        /// <summary>
        /// Retrieves a list of all Internet entities.
        /// </summary>
        /// <returns>List of Internet entities.</returns>
        Task<List<InternetEntityModelDTO>> GetInternetEntities();
        
        /// <summary>
        /// Updates the position of an Internet entity on the board.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated Internet entity.</returns>
        Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y);
    }
}
