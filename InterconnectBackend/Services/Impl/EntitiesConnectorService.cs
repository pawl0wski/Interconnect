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
        private readonly IVirtualSwitchEntityRepository _switchRepository;
        private readonly IInternetEntityRepository _internetRepository;
        private readonly IVirtualNetworkService _networkService;
        private readonly IVirtualSwitchConnector _virtualSwitchConnector;

        public EntitiesConnectorService(
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository,
            IInternetEntityRepository internetRepository,
            IVirtualNetworkService networkService,
            IVirtualSwitchConnector virtualSwitchConnector)
        {
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
            _internetRepository = internetRepository;
            _networkService = networkService;
            _virtualSwitchConnector = virtualSwitchConnector;
        } 

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            VirtualNetworkConnectionDTO? virtualNetworkConnection = null;

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                virtualNetworkConnection = await ConnectTwoVirtualMachines(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToVirtualSwitch(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.Internet, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToInternet(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualSwitch))
            {
                virtualNetworkConnection = await _virtualSwitchConnector.ConnectTwoVirtualSwitches(sourceEntityId, destinationEntityId);
            }

            if (EntitiesUtils.AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualSwitch, EntityType.Internet))
            {
                var (internetEntityId, virtualSwitchEntityId) = EntitiesUtils.GetInternetEntityIdFirst(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await _virtualSwitchConnector.ConnectVirtualSwitchToInternet(virtualSwitchEntityId, internetEntityId);
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

            var virtualSwitch = await _networkService.CreateVirtualSwitch(null);
            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(virtualSwitch.Uuid);

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

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualSwitch(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationVirtualSwitch = await _switchRepository.GetById(destinationEntityId);

            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(destinationVirtualSwitch.VirtualNetwork.Uuid);

            await _networkService.AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationVirtualSwitch.Id, EntityType.VirtualSwitch);

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
