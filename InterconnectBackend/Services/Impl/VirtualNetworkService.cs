using Mappers;
using Models;
using Models.Database;
using Models.DTO;
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
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IPacketSnifferService _packetSnifferService;

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository,
            IVirtualNetworkRepository networkRepository,
            IPacketSnifferService packetSnifferService)
        {
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
            _networkRepository = networkRepository;
            _packetSnifferService = packetSnifferService;
        }

        public async Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections()
        {
            var connections = await _connectionRepository.GetAll();
            return [.. connections.Select(VirtualNetworkEntityConnectionMapper.MapToDTO)];
        }

        public async Task<VirtualSwitchEntityDTO> CreateVirtualSwitch(string? name)
        {
            var virtualNetwork = await CreateSwitchVirtualNetwork();

            VirtualSwitchEntityModel virtualSwitchEntity;
            if (name is null)
            {
                virtualSwitchEntity = await _switchRepository.CreateInvisible(virtualNetwork);
            }
            else
            {
                virtualSwitchEntity = await _switchRepository.Create(name, virtualNetwork);
            }
            virtualSwitchEntity.VirtualNetwork = virtualNetwork;

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

        public async Task AttachNetworkInterfaceToVirtualMachine(int id, VirtualNetworkInterfaceCreateDefinition interfaceDefinition)
        {
            var virtualMachine = await _vmEntityRepository.GetById(id);
            var interfaceBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            interfaceBuilder.SetFromCreateDefinition(interfaceDefinition);
            var xmlDefinition = interfaceBuilder.Build();

            _wrapper.AttachDeviceToVirtualMachine(virtualMachine.VmUuid!.Value, xmlDefinition);

            virtualMachine.DeviceDefinition = xmlDefinition;
            await _vmEntityRepository.Update(virtualMachine);
        }

        public async Task UpdateNetworkForVirtualMachineNetworkInterface(int id, string networkName)
        {
            var vmEntity = await _vmEntityRepository.GetById(id);

            var builder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            builder.SetFromXml(vmEntity.DeviceDefinition!).SetNetworkName(networkName);
            var deviceDefinition = builder.Build();

            _wrapper.UpdateVmDevice(vmEntity.VmUuid!.Value, deviceDefinition);

            vmEntity.DeviceDefinition = deviceDefinition;
            await _vmEntityRepository.Update(vmEntity);
        }

        public async Task<VirtualNetworkModel> CreateSwitchVirtualNetwork()
        {
            var (networkName, bridgeName) = CreateNetworkAndBridgeNames();

            return await CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName
            });
        }

        public async Task<VirtualNetworkModel> CreateInternetVirtualNetwork()
        {
            var (networkName, bridgeName) = CreateNetworkAndBridgeNames();

            return await CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName,
                ForwardNat = true,
                IpAddress = "192.168.0.1",
                NetMask = "255.255.255.0"
            });
        }

        private (string, string) CreateNetworkAndBridgeNames()
        {
            var networkUuid = Guid.NewGuid();
            var networkName = VirtualNetworkUtils.GetNetworkNameFromUuid(networkUuid);
            var bridgeSuffixId = networkName.Split("-").Last();
            var bridgeName = $"ic{bridgeSuffixId}";

            return (networkName, bridgeName);
        }

        private async Task<VirtualNetworkModel> CreateVirtualNetwork(VirtualNetworkCreateDefinition definition)
        {
            var builder = new VirtualNetworkCreateDefinitionBuilder().SetFromCreateDefinition(definition);
            var networkDefinition = builder.Build();

            _wrapper.CreateVirtualNetwork(networkDefinition);

            _packetSnifferService.StartListeningForBridge(definition.BridgeName);

            return await _networkRepository.Create(definition.BridgeName, VirtualNetworkUtils.GetNetworkUuidFromName(definition.NetworkName), definition.IpAddress);
        }

        public Task<List<VirtualNetworkModel>> GetAllVirtualNetworks()
        {
            return _networkRepository.GetAll();
        }
    }
}
