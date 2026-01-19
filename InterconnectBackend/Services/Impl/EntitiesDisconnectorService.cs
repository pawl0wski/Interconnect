using Mappers;
using Models.DTO;
using Models.Enums;
using NativeLibrary.Wrappers;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    public class EntitiesDisconnectorService : IEntitiesDisconnectorService
    {
        private readonly IVirtualizationWrapper _wrapper;
        private readonly IVirtualMachineEntityRepository _vmEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IVirtualNetworkNodeConnector _virtualNetworkNodeConnector;
        private readonly IVirtualMachineEntityNetworkInterfaceRepository _networkInterfaceRepository;

        public EntitiesDisconnectorService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeRepository,
            IVirtualNetworkRepository networkRepository,
            IVirtualNetworkNodeConnector virtualNetworkNodeConnector,
            IVirtualMachineEntityNetworkInterfaceRepository networkInterfaceRepository
            )
        {
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _virtualNetworkNodeRepository = virtualNetworkNodeRepository;
            _networkRepository = networkRepository;
            _virtualNetworkNodeConnector = virtualNetworkNodeConnector;
            _networkInterfaceRepository = networkInterfaceRepository;
        }

        public async Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId)
        {
            var connection = await _connectionRepository.GetById(connectionId);

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualNetworkNode))
            {
                var (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);

                await DisconnectVirtualMachineFromVirtualNetworkNode(connectionId, sourceEntityId, destinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                await DisconnectVirtualMachineFromVirtualMachine(connectionId, connection.SourceEntityId, connection.DestinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualNetworkNode, EntityType.VirtualNetworkNode))
            {
                await _virtualNetworkNodeConnector.DisconnectTwoVirtualNetworkNodes(connectionId, connection.SourceEntityId, connection.DestinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualNetworkNode, EntityType.Internet))
            {
                var (internetEntityId, virtualNetworkNodeEntityId) = EntitiesUtils.GetInternetEntityIdFirst(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);
                await _virtualNetworkNodeConnector.DisconnectVirtualNetworkNodeFromInternet(connectionId, virtualNetworkNodeEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.Internet))
            {
                var (virtualMachineEntityId, internetEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);
                await DisconnectVirtualMachineFromInternet(connectionId, virtualMachineEntityId, internetEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            throw new NotImplementedException("Disconnect entities not implemented with provided entity types");
        }

        public async Task DisconnectVirtualMachineFromVirtualNetworkNode(int connectionId, int sourceEntityId, int destinationEntityId)
        {
            var virtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var networkInterface = await _networkInterfaceRepository.GetByIds(sourceEntityId, connectionId);

            if (networkInterface is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to anything");
            }

            _wrapper.DetachDeviceFromVirtualMachine(virtualMachine.VmUuid!.Value, networkInterface.Definition);
                
            await _connectionRepository.Remove(connectionId);
        }

        public async Task DisconnectVirtualMachineFromVirtualMachine(int connectionId, int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var sourceVirtualMachineNetworkInterface = await _networkInterfaceRepository.GetByIds(sourceEntityId, connectionId);

            var destinationVirtualMachine = await _vmEntityRepository.GetById(destinationEntityId);
            var destinationVirtualMachineNetworkInterface = await _networkInterfaceRepository.GetByIds(destinationEntityId, connectionId);

            var connection = await _connectionRepository.GetById(connectionId);

            if (sourceVirtualMachineNetworkInterface is null
                || destinationVirtualMachineNetworkInterface is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to anything");
            }

            var networkName = VirtualNetworkDefinitionUtils.GetNetworkNameFromDefinition(sourceVirtualMachineNetworkInterface.Definition);

            if (networkName is null)
            {
                throw new NullReferenceException("Can't get network name from network definition");
            }

            var virtualNetworkUuid = Guid.Parse(networkName.Replace("InterconnectNode-", ""));
            var virtualNetwork = await _networkRepository.GetByUuidWithVirtualNetworkNodes(virtualNetworkUuid);
            var virtualNetworkNode = virtualNetwork.VirtualNetworkNodes.First();

            _wrapper.DetachDeviceFromVirtualMachine(sourceVirtualMachine.VmUuid!.Value, sourceVirtualMachineNetworkInterface.Definition);
            _wrapper.DetachDeviceFromVirtualMachine(destinationVirtualMachine.VmUuid!.Value, destinationVirtualMachineNetworkInterface.Definition);
            _wrapper.DestroyNetwork(VirtualNetworkUtils.GetNetworkNameFromUuid(virtualNetworkNode.VirtualNetwork.Uuid));

            await _virtualNetworkNodeRepository.Remove(virtualNetworkNode.Id);
            await _networkRepository.Remove(virtualNetwork.Id);
            await _connectionRepository.Remove(connectionId);
        }

        public async Task DisconnectVirtualMachineFromInternet(int connectionId, int virtualMachineEntityId, int internetEntityId)
        {
            var virtualMachine = await _vmEntityRepository.GetById(virtualMachineEntityId);
            var networkInterface = await _networkInterfaceRepository.GetByIds(virtualMachineEntityId, connectionId);

            if (networkInterface is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to provided entity");
            }

            _wrapper.DetachDeviceFromVirtualMachine(virtualMachine.VmUuid!.Value, networkInterface.Definition);

            await _networkInterfaceRepository.Remove(networkInterface);
            await _connectionRepository.Remove(connectionId);
        }
    }
}
