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
        private readonly IVirtualSwitchEntityRepository _switchRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IVirtualSwitchConnector _virtualSwitchConnector;

        public EntitiesDisconnectorService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository,
            IVirtualNetworkRepository networkRepository,
            IVirtualSwitchConnector virtualSwitchConnector)
        {
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
            _networkRepository = networkRepository;
            _virtualSwitchConnector = virtualSwitchConnector;
        }

        public async Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId)
        {
            var connection = await _connectionRepository.GetById(connectionId);

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualSwitch))
            {
                var (sourceEntityId, destinationEntityId) = EntitiesUtils.GetVirtualMachineEntityIdFirst(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);

                await DisconnectVirtualMachineFromVirtualSwitch(connectionId, sourceEntityId, destinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                await DisconnectVirtualMachineFromVirtualMachine(connectionId, connection.SourceEntityId, connection.DestinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualSwitch))
            {
                await _virtualSwitchConnector.DisconnectTwoVirtualSwitches(connectionId, connection.SourceEntityId, connection.DestinationEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            if (EntitiesUtils.AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualSwitch, EntityType.Internet))
            {
                var (internetEntityId, virtualSwitchEntityId) = EntitiesUtils.GetInternetEntityIdFirst(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);
                await _virtualSwitchConnector.DisconnectVirtualSwitchFromInternet(connectionId, virtualSwitchEntityId);
                return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
            }

            throw new NotImplementedException("Disconnect entities not implemented with provided entity types");
        }

        public async Task DisconnectVirtualMachineFromVirtualSwitch(int connectionId, int sourceEntityId, int destinationEntityId)
        {
            var virtualMachine = await _vmEntityRepository.GetById(sourceEntityId);

            if (virtualMachine.DeviceDefinition is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to anything");
            }

            _wrapper.DetachDeviceFromVirtualMachine(virtualMachine.VmUuid!.Value, virtualMachine.DeviceDefinition);

            await _connectionRepository.Remove(connectionId);
            virtualMachine.DeviceDefinition = null;
            await _vmEntityRepository.Update(virtualMachine);
        }

        public async Task DisconnectVirtualMachineFromVirtualMachine(int connectionId, int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityRepository.GetById(sourceEntityId);
            var destinationVirtualMachine = await _vmEntityRepository.GetById(destinationEntityId);
            var connection = await _connectionRepository.GetById(connectionId);

            if (sourceVirtualMachine.DeviceDefinition is null || destinationVirtualMachine.DeviceDefinition is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to anything");
            }

            var networkName = VirtualNetworkDefinitionUtils.GetNetworkNameFromDefinition(sourceVirtualMachine.DeviceDefinition);

            if (networkName is null)
            {
                throw new NullReferenceException("Can't get network name from network definition");
            }

            var virtualNetworkUuid = Guid.Parse(networkName.Replace("InterconnectSwitch-", ""));
            var virtualNetwork = await _networkRepository.GetByUuidWithVirtualSwitches(virtualNetworkUuid);
            var virtualSwitch = virtualNetwork.VirtualSwitches.First();

            _wrapper.DetachDeviceFromVirtualMachine(sourceVirtualMachine.VmUuid!.Value, sourceVirtualMachine.DeviceDefinition);
            _wrapper.DetachDeviceFromVirtualMachine(destinationVirtualMachine.VmUuid!.Value, destinationVirtualMachine.DeviceDefinition);

            _wrapper.DestroyNetwork(VirtualNetworkUtils.GetNetworkNameFromUuid(virtualSwitch.VirtualNetwork.Uuid));

            await _switchRepository.Remove(virtualSwitch.Id);
            await _networkRepository.Remove(virtualNetwork.Id);
            await _connectionRepository.Remove(connectionId);

            sourceVirtualMachine.DeviceDefinition = null;
            destinationVirtualMachine.DeviceDefinition = null;
            await _vmEntityRepository.Update(sourceVirtualMachine);
            await _vmEntityRepository.Update(destinationVirtualMachine);
        }
    }
}
