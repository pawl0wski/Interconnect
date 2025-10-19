using Mappers;
using Models;
using Models.DTO;
using Models.Enums;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
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

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId)
        {
            var sourceEntity = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationEntity = await _vmEntityRepository.GetById(destinationEntityId);

            var virtualNetworkNode = await _networkService.CreateVirtualNetworkNode(null);
            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(virtualNetworkNode.Uuid);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceEntity.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });
            await _networkService.AttachNetworkInterfaceToVirtualMachine(destinationEntity.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connectionModel = await _connectionRepository.Create(sourceEntity.Id, EntityType.VirtualMachine, destinationEntity.Id, EntityType.VirtualMachine);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualNetworkNode(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationVirtualNetworkNode = await _virtualNetworkNodeRepository.GetById(destinationEntityId);

            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(destinationVirtualNetworkNode.VirtualNetwork.Uuid);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationVirtualNetworkNode.Id, EntityType.VirtualNetworkNode);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationInternet = await _internetRepository.GetById(destinationEntityId);

            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(destinationInternet.VirtualNetwork.Uuid);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationEntityId, EntityType.Internet);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }
    }
}
