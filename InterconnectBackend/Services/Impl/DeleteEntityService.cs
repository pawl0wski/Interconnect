using Models.Enums;
using Repositories;

namespace Services.Impl
{
    /// <summary>
    /// Service responsible for deleting entities from the system.
    /// </summary>
    public class DeleteEntityService : IDeleteEntityService
    {
        private readonly IInternetEntityRepository _internetEntityRepository;
        private readonly IVirtualMachineEntityRepository _virtualMachineEntityRepository;
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _virtualNetworkConnectionRepository;
        private readonly IEntitiesDisconnectorService _entitiesDisconnectorService;

        /// <summary>
        /// Initializes a new instance of the DeleteEntityService.
        /// </summary>
        /// <param name="internetEntityRepository">Repository for Internet entities.</param>
        /// <param name="virtualMachineEntityRepository">Repository for virtual machine entities.</param>
        /// <param name="virtualNetworkNodeEntityRepository">Repository for virtual network node entities.</param>
        /// <param name="virtualNetworkConnectionRespository">Repository for virtual network connections.</param>
        /// <param name="entitiesDisconnectorService">Service for disconnecting entities.</param>
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

        /// <summary>
        /// Deletes an Internet entity.
        /// </summary>
        /// <param name="id">Entity identifier to delete.</param>
        public async Task DeleteInternetEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.Internet);

            await _internetEntityRepository.Remove(id);
        }

        /// <summary>
        /// Deletes a virtual network node entity.
        /// </summary>
        /// <param name="id">Entity identifier to delete.</param>
        public async Task DeleteVirtualNetworkNodeEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.VirtualNetworkNode);

            await _virtualNetworkNodeEntityRepository.Remove(id);
        }

        /// <summary>
        /// Deletes a virtual machine entity.
        /// </summary>
        /// <param name="id">Entity identifier to delete.</param>
        public async Task DeleteVirtualMachineEntity(int id)
        {
            await DisconnectAllConnectionsFromEntity(id, EntityType.VirtualMachine);

            await _virtualMachineEntityRepository.Remove(id);
        }

        private async Task DisconnectAllConnectionsFromEntity(int id, EntityType type)
        {
            var connections = await _virtualNetworkConnectionRepository.GetUsingEntityId(id, type);

            foreach (var connection in connections)
            {
                await _entitiesDisconnectorService.DisconnectEntities(connection.Id);
            }
        }
    }
}
