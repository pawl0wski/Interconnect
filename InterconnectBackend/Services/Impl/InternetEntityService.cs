using Mappers;
using Models.DTO;
using Repositories;

namespace Services.Impl
{
    public class InternetEntityService : IInternetEntityService
    {
        private readonly IInternetEntityRepository _internetRepository;
        private readonly IVirtualNetworkService _virtualNetworkService;

        public InternetEntityService(
            IInternetEntityRepository internetRepository,
            IVirtualNetworkService virtualNetworkService
            )
        {
            _internetRepository = internetRepository;
            _virtualNetworkService = virtualNetworkService;
        }
        public async Task<InternetEntityModelDTO> CreateInternet()
        {
            var virtualNetwork = await _virtualNetworkService.CreateInternetVirtualNetwork();

            var internetEntity = await _internetRepository.Create(virtualNetwork);
            return InternetEntityMapper.MapToDTO(internetEntity);
        }

        public async Task<List<InternetEntityModelDTO>> GetInternetEntities()
        {
            var internetEntities = await _internetRepository.GetAll();

            return [.. internetEntities.Select(InternetEntityMapper.MapToDTO)];
        }

        public async Task<InternetEntityModelDTO> UpdateInternetEntityPosition(int entityId, int x, int y)
        {
            var model = await _internetRepository.UpdatePosition(entityId, x, y);

            return InternetEntityMapper.MapToDTO(model);
        }
    }
}
