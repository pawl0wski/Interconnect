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
        private readonly IVirtualMachineEntityService _vmEntityService;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualSwitchEntityRepository _switchRepository;
        private readonly IInternetEntityRepository _internetRepository;

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityService vmEntityService,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository,
            IInternetEntityRepository internetRepository)
        {
            _wrapper = wrapper;
            _vmEntityService = vmEntityService;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
            _internetRepository = internetRepository;
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId)
        {
            var sourceEntity = await _vmEntityService.GetById(sourceEntityId);
            var destinationEntity = await _vmEntityService.GetById(destinationEntityId);

            var virtualSwitch = await CreateVirtualSwitch(null);
            var networkName = GetNetworkNameFromUuid(virtualSwitch.Uuid);

            EnsureVmUuidNotNull(sourceEntity);
            EnsureVmUuidNotNull(destinationEntity);


            AttachNetworkInterfaceToVirtualMachine(sourceEntity.VmUuid!.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });
            AttachNetworkInterfaceToVirtualMachine(destinationEntity.VmUuid!.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connectionModel = await _connectionRepository.Create(sourceEntity.Id, EntityType.VirtualMachine, destinationEntity.Id, EntityType.VirtualMachine);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connectionModel);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualSwitch(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityService.GetById(sourceEntityId);
            var destinationVirtualSwitch = await _switchRepository.GetById(destinationEntityId);

            var networkName = GetNetworkNameFromUuid(destinationVirtualSwitch.Uuid);

            EnsureVmUuidNotNull(sourceVirtualMachine);

            AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.VmUuid!.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationVirtualSwitch.Id, EntityType.VirtualSwitch);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
        }

        public async Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId)
        {
            var sourceVirtualMachine = await _vmEntityService.GetById(sourceEntityId);
            var destinationInternet = await _internetRepository.GetById(destinationEntityId);

            var networkName = GetNetworkNameFromUuid(destinationInternet.Uuid);

            EnsureVmUuidNotNull(sourceVirtualMachine);

            AttachNetworkInterfaceToVirtualMachine(sourceVirtualMachine.VmUuid!.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            var connection = await _connectionRepository.Create(sourceVirtualMachine.Id, EntityType.VirtualMachine, destinationEntityId, EntityType.Internet);

            return VirtualNetworkEntityConnectionMapper.MapToDTO(connection);
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

            CreateVirtualNetwork(new VirtualNetworkCreateDefinition { NetworkName = networkName, BridgeName = bridgeName });

            VirtualSwitchEntityModel virtualSwitchEntity;
            if (name is null)
            {
                virtualSwitchEntity = await _switchRepository.CreateInvisible(bridgeName, networkUuid);
            }
            else
            {
                virtualSwitchEntity = await _switchRepository.Create(name, bridgeName, networkUuid);
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

            CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName,
                ForwardNat = true,
                IpAddress = "192.168.0.1",
                NetMask = "255.255.255.0"
            });

            var internetEntity = await _internetRepository.Create(bridgeName, networkUuid);
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

        private void CreateVirtualNetwork(VirtualNetworkCreateDefinition definition)
        {
            var builder = new VirtualNetworkCreateDefinitionBuilder().SetFromCreateDefinition(definition);
            var networkDefinition = builder.Build();

            _wrapper.CreateVirtualNetwork(networkDefinition);
        }

        private void AttachNetworkInterfaceToVirtualMachine(Guid uuid, VirtualNetworkInterfaceCreateDefinition interfaceDefinition)
        {
            var interfaceBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            interfaceBuilder.SetFromCreateDefinition(interfaceDefinition);
            var xmlDefinition = interfaceBuilder.Build();

            _wrapper.AttachDeviceToVirtualMachine(uuid, xmlDefinition);
        }

        private string GetNetworkNameFromUuid(Guid uuid) =>
            $"InterconnectSwitch-{uuid}";

        private void EnsureVmUuidNotNull(VirtualMachineEntityDTO entity)
        {
            if (entity.VmUuid is null)
            {
                throw new NullReferenceException("Source entity vmuuid is null");
            }
        }
    }
}
