using Mappers;
using Models;
using Models.Database;
using Models.DTO;
using Models.Enums;
using NativeLibrary.Wrappers;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualNetworkService : IVirtualNetworkService
    {
        private readonly IVirtualizationWrapper _wrapper;
        private readonly IVirtualMachineEntityRepository _vmEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualSwitchEntityRepository _switchRepository;
        private readonly IInternetEntityRepository _internetRepository;
        private readonly IVirtualNetworkRepository _networkRepository;

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository,
            IInternetEntityRepository internetRepository,
            IVirtualNetworkRepository networkRepository)
        {
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
            _internetRepository = internetRepository;
            _networkRepository = networkRepository;
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            VirtualNetworkConnectionDTO? virtualNetworkConnection = null;

            if (AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                virtualNetworkConnection = await ConnectTwoVirtualMachines(sourceEntityId, destinationEntityId);
            }

            if (AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = ResolveEntityIdsOrder(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToVirtualSwitch(sourceEntityId, destinationEntityId);
            }

            if (AreTypes(sourceEntityType, destinationEntityType, EntityType.Internet, EntityType.VirtualMachine))
            {
                (sourceEntityId, destinationEntityId) = ResolveEntityIdsOrder(sourceEntityId, sourceEntityType, destinationEntityId, destinationEntityType);

                virtualNetworkConnection = await ConnectVirtualMachineToInternet(sourceEntityId, destinationEntityId);
            }

            if (AreTypes(sourceEntityType, destinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualSwitch))
            {
                virtualNetworkConnection = await ConnectTwoVirtualSwitches(sourceEntityId, destinationEntityId);
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

            var virtualSwitch = await CreateVirtualSwitch(null);
            var networkName = GetNetworkNameFromUuid(virtualSwitch.Uuid);

            await AttachNetworkInterfaceToVirtualMachine(sourceEntity.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });
            await AttachNetworkInterfaceToVirtualMachine(destinationEntity.Id, new VirtualNetworkInterfaceCreateDefinition
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

            var networkName = GetNetworkNameFromUuid(destinationVirtualSwitch.VirtualNetwork.Uuid);

            await AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, new VirtualNetworkInterfaceCreateDefinition
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

            var networkName = GetNetworkNameFromUuid(destinationInternet.VirtualNetwork.Uuid);

            await AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.Id, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationEntityId, EntityType.Internet);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualSwitches(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualSwitch = await _switchRepository.GetById(sourceEntityId);
            var destinationVirtualSwitch = await _switchRepository.GetById(destinationEntityId);

            Dictionary<int, int> networks = new();
            Queue<VirtualNetworkEntityConnectionModel> connetionsToVisit = new();
            HashSet<VirtualNetworkEntityConnectionModel> visitedConnections = new();
            HashSet<int> visitedEntityIds = new();
            HashSet<VirtualSwitchEntityModel> virtualSwitches = new();
            HashSet<VirtualMachineEntityModel> virtualMachines = new();
            Dictionary<int, VirtualNetworkEntityConnectionModel> virtualMachineConnection = new();

            networks[sourceVirtualSwitch.VirtualNetwork.Id] = 1;
            virtualSwitches.Add(sourceVirtualSwitch);
            networks[destinationVirtualSwitch.VirtualNetwork.Id] = 1;
            virtualSwitches.Add(destinationVirtualSwitch);

            (await _connectionRepository.GetUsingEntityId(sourceEntityId, EntityType.VirtualSwitch)).ForEach((c) =>
            {
                connetionsToVisit.Enqueue(c);
                visitedConnections.Add(c);
            });


            (await _connectionRepository.GetUsingEntityId(destinationEntityId, EntityType.VirtualSwitch)).ForEach((c) =>
            {
                connetionsToVisit.Enqueue(c);
                visitedConnections.Add(c);
            });

            while (connetionsToVisit.Count > 0)
            {
                var connection = connetionsToVisit.Dequeue();
                var targetEntityId = visitedEntityIds.Contains(connection.SourceEntityId) ? connection.DestinationEntityId : connection.SourceEntityId;
                var targetEntityType = visitedEntityIds.Contains(connection.SourceEntityId) ? connection.DestinationEntityType : connection.SourceEntityType;

                if (visitedEntityIds.Contains(targetEntityId))
                {
                    continue;
                }

                if (targetEntityType == EntityType.VirtualMachine)
                {
                    virtualMachines.Add(await _vmEntityRepository.GetById(targetEntityId));
                    virtualMachineConnection[targetEntityId] = connection;
                }

                if (targetEntityType == EntityType.VirtualSwitch)
                {
                    var virtualSwitch = await _switchRepository.GetById(targetEntityId);
                    networks[virtualSwitch.VirtualNetwork.Id] = networks.GetValueOrDefault(virtualSwitch.VirtualNetwork.Id) + 1;
                    virtualSwitches.Add(virtualSwitch);
                }

                if (targetEntityType == EntityType.Internet)
                {
                    var internet = await _internetRepository.GetById(targetEntityId);
                    networks[internet.VirtualNetwork.Id] = 9999;
                }

                var connections = await _connectionRepository.GetUsingEntityId(targetEntityId, targetEntityType);
                connections.Where(c => !visitedConnections.Contains(c)).ToList().ForEach((c) =>
                {
                    connetionsToVisit.Enqueue(c);
                    visitedConnections.Add(c);
                });
            }

            var popularNetworkId = networks
                .OrderByDescending(n => n.Value)
                .First()
                .Key;
            var popularNetwork = await _networkRepository.GetById(popularNetworkId);

            foreach (var virtualSwitch in virtualSwitches)
            {
                await _switchRepository.UpdateNetwork(virtualSwitch.Id, popularNetwork);
            }

            foreach (var virtualMachine in virtualMachines)
            {
                await UpdateNetworkForVirtualMachineNetworkInterface(virtualMachine.Id, GetNetworkNameFromUuid(popularNetwork.Uuid));
            }

            var connectionModel = await _connectionRepository.Create(sourceEntityId, EntityType.VirtualSwitch, destinationEntityId, EntityType.VirtualSwitch);
            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId)
        {
            var connection = await _connectionRepository.GetById(connectionId);

            if (AreTypes(connection.SourceEntityType, connection.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualSwitch))
            {
                var (sourceEntityId, destinationEntityId) = ResolveEntityIdsOrder(connection.SourceEntityId, connection.SourceEntityType, connection.DestinationEntityId, connection.DestinationEntityType);

                await DisconnectVirtualMachineToVirtualSwitch(connectionId, sourceEntityId, destinationEntityId);
            }

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }

        public async Task DisconnectVirtualMachineToVirtualSwitch(int connectionId, int sourceEntityId, int destinationEntityId)
        {
            var virtualMachine = await _vmEntityRepository.GetById(sourceEntityId);

            if (virtualMachine.DeviceDefinition is null)
            {
                throw new NullReferenceException("VirtualMachine is not connected to anything");
            }

            _wrapper.DetachDeviceFromVirtualMachine(virtualMachine.VmUuid!.Value, virtualMachine.DeviceDefinition);

            await _connectionRepository.Remove(connectionId);
        }

        public async Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections()
        {
            var connections = await _connectionRepository.GetAll();
            return [.. connections.Select(VirtualNetworkEntityConnectionMapper.MapToDTO)];
        }

        public async Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name)
        {
            var networkUuid = Guid.NewGuid();
            var networkName = GetNetworkNameFromUuid(networkUuid);
            var bridgeSuffixId = networkName.Split("-").Last();
            var bridgeName = $"ic{bridgeSuffixId}";

            var virtualNetwork = await CreateVirtualNetwork(new VirtualNetworkCreateDefinition { NetworkName = networkName, BridgeName = bridgeName }, networkUuid);

            VirtualSwitchEntityModel virtualSwitchEntity;
            if (name is null)
            {
                virtualSwitchEntity = await _switchRepository.CreateInvisible(virtualNetwork);
            }
            else
            {
                virtualSwitchEntity = await _switchRepository.Create(name, virtualNetwork);
            }

            return VirtualSwitchEntityMapper.MapToDTO(virtualSwitchEntity);
        }

        public async Task<List<VirtualSwitchEntityDTO>> GetVisibleVirtualSwitchEntities()
        {
            var virtualSwitches = await _switchRepository.GetVisible();
            return [.. virtualSwitches.Select(VirtualSwitchEntityMapper.MapToDTO)];
        }

        public async Task<VirtualSwitchEntityDTO> UpdateVirtualSwitchEntityPosition(int entityId, int x, int y)
        {
            var model = await _switchRepository.UpdateEntityPosition(entityId, x, y);

            return VirtualSwitchEntityMapper.MapToDTO(model);
        }

        public async Task<InternetEntityModelDTO> CreateInternet()
        {
            var networkUuid = Guid.NewGuid();
            var networkName = GetNetworkNameFromUuid(networkUuid);
            var bridgeSuffixId = networkName.Split("-").Last();
            var bridgeName = $"ic{bridgeSuffixId}";

            var virtualNetwork = await CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName,
                ForwardNat = true,
                IpAddress = "192.168.0.1",
                NetMask = "255.255.255.0"
            }, networkUuid);

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

        private async Task<VirtualNetworkModel> CreateVirtualNetwork(VirtualNetworkCreateDefinition definition, Guid uuid)
        {
            var builder = new VirtualNetworkCreateDefinitionBuilder().SetFromCreateDefinition(definition);
            var networkDefinition = builder.Build();

            _wrapper.CreateVirtualNetwork(networkDefinition);

            return await _networkRepository.Create(definition.BridgeName, uuid);
        }

        private async Task AttachNetworkInterfaceToVirtualMachine(int id, VirtualNetworkInterfaceCreateDefinition interfaceDefinition)
        {
            var virtualMachine = await _vmEntityRepository.GetById(id);
            var interfaceBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            interfaceBuilder.SetFromCreateDefinition(interfaceDefinition);
            var xmlDefinition = interfaceBuilder.Build();

            _wrapper.AttachDeviceToVirtualMachine(virtualMachine.VmUuid!.Value, xmlDefinition);

            virtualMachine.DeviceDefinition = xmlDefinition;
            await _vmEntityRepository.Update(virtualMachine);
        }

        private async Task UpdateNetworkForVirtualMachineNetworkInterface(int id, string networkName)
        {
            var vmEntity = await _vmEntityRepository.GetById(id);

            var builder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            builder.SetFromXml(vmEntity.DeviceDefinition!).SetNetworkName(networkName);
            var deviceDefinition = builder.Build();

            _wrapper.UpdateVmDevice(vmEntity.VmUuid!.Value, deviceDefinition);

            vmEntity.DeviceDefinition = deviceDefinition;
            await _vmEntityRepository.Update(vmEntity);
        }

        private (int sourceEntityId, int destinationEntityId) ResolveEntityIdsOrder(int sourceEntityId, EntityType sourceEntityType, int destinationEntityId, EntityType destinationEntityType)
        {
            if (sourceEntityType != EntityType.VirtualMachine)
            {
                (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
            }

            return (sourceEntityId, destinationEntityId);
        }

        private bool AreTypes(EntityType sourceEntityType, EntityType destinationEntityType, EntityType firstEntityType, EntityType secondEntityType)
        {
            return (sourceEntityType == firstEntityType && destinationEntityType == secondEntityType) || (sourceEntityType == secondEntityType && destinationEntityType == firstEntityType);
        }

        private string GetNetworkNameFromUuid(Guid uuid) =>
            $"InterconnectSwitch-{uuid}";
    }
}
