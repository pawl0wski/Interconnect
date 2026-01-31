using Models.Enums;
using Repositories;

namespace Services.Impl
{
    public class DeleteEntityService : IDeleteEntityService
    {
        private readonly IInternetEntityRepository _internetEntityRepository;
        private readonly IVirtualMachineEntityRepository _virtualMachineEntityRepository;
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _virtualNetworkConnectionRepository;
        private readonly IEntitiesDisconnectorService _entitiesDisconnectorService;

        public DeleteEntityService(
            IInternetEntityRepository internetEntityRepository,
            IVirtualMachineEntityRepository virtualMachineEntityRepository,
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeEntityRepository,
            IVirtualNetworkConnectionRepository virtualNetworkConnectionRespository,
            IEntitiesDisconnectorService entitiesDisconnectorService)
        {
            _internetEntityRepository = internetEntityRepository;
            _virtualMachineEntityRepository = virtualMachineEntityRepository;
            _virtualNetworkNodeEntityRepository = virtualNetworkNodeEntityRepository;
            _virtualNetworkConnectionRepository = virtualNetworkConnectionRespository;
            _entitiesDisconnectorService = entitiesDisconnectorService;
        }

        public async Task DeleteInternetEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.Internet);

            await _internetEntityRepository.Remove(id);
        }

        public async Task DeleteVirtualNetworkNodeEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.VirtualNetworkNode);

            await _virtualNetworkNodeEntityRepository.Remove(id);
        }

        public async Task DeleteVirtualMachineEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.Internet);

            await _virtualMachineEntityRepository.Remove(id);
        }

        private async Task DisconnectAllConnectionsFromEntity(int id, EntityType type)
        {
            var connections = await _virtualNetworkConnectionRepository.GetUsingEntityId(id, EntityType.Internet);

            foreach (var connection in connections)
            {
                await _entitiesDisconnectorService.DisconnectEntities(connection.Id);
            }
        }
    }
}
