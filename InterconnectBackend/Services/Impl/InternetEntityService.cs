using Mappers;
using Models.DTO;
using Repositories;

namespace Services.Impl
{
    /// <summary>
    /// Service managing Internet entities.
    /// </summary>
    public class InternetEntityService : IInternetEntityService
    {
        private readonly IInternetEntityRepository _internetRepository;
        private readonly IVirtualNetworkService _virtualNetworkService;

        /// <summary>
        /// Initializes a new instance of the InternetEntityService.
        /// </summary>
        /// <param name="internetRepository">Repository for Internet entities.</param>
        /// <param name="virtualNetworkService">Service for managing virtual networks.</param>
        public InternetEntityService(
            IInternetEntityRepository internetRepository,
            IVirtualNetworkService virtualNetworkService
            )
        {
            _internetRepository = internetRepository;
            _virtualNetworkService = virtualNetworkService;
        }

        /// <summary>
        /// Creates a new Internet entity.
        /// </summary>
        /// <returns>Created Internet entity.</returns>
        public async Task<InternetEntityModelDTO> CreateInternet()
        {
            var virtualNetwork = await _virtualNetworkService.CreateInternetVirtualNetwork();

            var internetEntity = await _internetRepository.Create(virtualNetwork);
            return InternetEntityMapper.MapToDTO(internetEntity);
        }

        /// <summary>
        /// Retrieves a list of all Internet entities.
        /// </summary>
        /// <returns>List of Internet entities.</returns>
        public async Task<List<InternetEntityModelDTO>> GetInternetEntities()
        {
            var internetEntities = await _internetRepository.GetAll();

            return [.. internetEntities.Select(InternetEntityMapper.MapToDTO)];
        }

        /// <summary>
        /// Updates the position of an Internet entity on the board.
        /// </summary>
        /// <param name="entityId">Entity identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated Internet entity.</returns>
        public async Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y)
        {
            var model = await _internetRepository.UpdatePosition(entityId, x, y);

            return InternetEntityMapper.MapToDTO(model);
        }
    }
}
