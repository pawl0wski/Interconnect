using Mappers;
using Models;
using Models.DTO;
using Models.Enums;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    /// <summary>
    /// Service responsible for connecting network entities.
    /// </summary>
    public class EntitiesConnectorService : IEntitiesConnectorService
    {
        private readonly IVirtualMachineEntityRepository _vmEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeRepository;
        private readonly IInternetEntityRepository _internetRepository;
        private readonly IVirtualNetworkService _networkService;
        private readonly IVirtualNetworkNodeConnector _virtualNetworkNodeConnector;

        public EntitiesConnectorService(
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeRepository,
            IInternetEntityRepository internetRepository,
            IVirtualNetworkService networkService,
            IVirtualNetworkNodeConnector virtualNetworkNodeConnector)
        {
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _virtualNetworkNodeRepository = virtualNetworkNodeRepository;
            _internetRepository = internetRepository;
            _networkService = networkService;
            _virtualNetworkNodeConnector = virtualNetworkNodeConnector;
        }

        /// <summary>
        /// Connects two entities of different types.
        /// </summary>
        /// <param name="sourceEntityId">Source entity identifier.</param>
        /// <param name="sourceEntityType">Source entity type.</param>
        /// <param name="destinationEntitiyId">Destination entity identifier.</param>
        /// <param name="destinationEntityType">Destination entity type.</param>
        /// <returns>Created connection data.</returns>
        public async Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            VirtualNetworkConnectionDTO? virtualNetworkConnection = null;

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                virtualNetworkConnection = await ConnectTwoVirtualMachines(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualNetworkNode, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToVirtualNetworkNode(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.Internet, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToInternet(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualNetworkNode, EntityType.VirtualNetworkNode))
            {
                virtualNetworkConnection = await _virtualNetworkNodeConnector.ConnectTwoVirtualNetworkNodes(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualNetworkNode, EntityType.Internet))
            {
                var (internetEntityId, virtualNetworkNodeEntityId) = EntitiesUtils.GetInternetEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await _virtualNetworkNodeConnector.ConnectVirtualNetworkNodeToInternet(virtualNetworkNodeEntityId, internetEntityId);
            }

            if (virtualNetworkConnection is null)
            {
                throw new NotImplementedException("Unsuported entity types");
            }

            return virtualNetworkConnection;
        }

        /// <summary>
        /// Connects two virtual machines.
        /// </summary>
        /// <param name="sourceEntityId">First virtual machine identifier.</param>
        /// <param name="destinationEntityId">Second virtual machine identifier.</param>
        /// <returns>Created connection data.</returns>
        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId)
        {
            var sourceEntity = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationEntity = await _vmEntityRepository.GetById(destinationEntityId);

            var virtualNetworkNode = await _networkService.CreateVirtualNetworkNode(null);
            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(virtualNetworkNode.Uuid);

            var connectionModel = await _connectionRepository.Create(sourceEntity.Id, EntityType.VirtualMachine, destinationEntity.Id, EntityType.VirtualMachine);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceEntity.Id, connectionModel.Id,
                new VirtualNetworkInterfaceCreateDefinition
                {
                    NetworkName = networkName,
                    MacAddress = MacAddressGenerator.Generate()
                });
            await _networkService.AttachNetworkInterfaceToVirtualMachine(destinationEntity.Id, connectionModel.Id,
                new VirtualNetworkInterfaceCreateDefinition
                {
                    NetworkName = networkName,
                    MacAddress = MacAddressGenerator.Generate()
                });

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        /// <summary>
        /// Connects a virtual machine to a virtual network node.
        /// </summary>
        /// <param name="sourceEntityId">Virtual machine identifier.</param>
        /// <param name="destinationEntityId">Network node identifier.</param>
        /// <returns>Created connection data.</returns>
        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualNetworkNode(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationVirtualNetworkNode = await _virtualNetworkNodeRepository.GetById(destinationEntityId);

            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(destinationVirtualNetworkNode.VirtualNetwork.Uuid);

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationVirtualNetworkNode.Id, EntityType.VirtualNetworkNode);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, connection.Id,
                new VirtualNetworkInterfaceCreateDefinition
                {
                    NetworkName = networkName,
                    MacAddress = MacAddressGenerator.Generate()
                });

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }

        /// <summary>
        /// Connects a virtual machine to the Internet.
        /// </summary>
        /// <param name="sourceEntityId">Virtual machine identifier.</param>
        /// <param name="destinationEntityId">Internet entity identifier.</param>
        /// <returns>Created connection data.</returns>
        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationInternet = await _internetRepository.GetById(destinationEntityId);

            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(destinationInternet.VirtualNetwork.Uuid);

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationEntityId, EntityType.Internet);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, connection.Id,
                new VirtualNetworkInterfaceCreateDefinition
                {
                    NetworkName = networkName,
                    MacAddress = MacAddressGenerator.Generate()
                });

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }
    }
}
