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
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IPacketSnifferService _packetSnifferService;

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeRepository,
            IVirtualNetworkRepository networkRepository,
            IPacketSnifferService packetSnifferService)
        {
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _virtualNetworkNodeRepository = virtualNetworkNodeRepository;
            _networkRepository = networkRepository;
            _packetSnifferService = packetSnifferService;
        }

        public async Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections()
        {
            var connections = await _connectionRepository.GetAll();
            return [.. connections.Select(VirtualNetworkEntityConnectionMapper.MapToDTO)];
        }

        public async Task<VirtualNetworkNodeEntityDTO> CreateVirtualNetworkNode(string? name)
        {
            var virtualNetwork = await CreateNodeVirtualNetwork();

            VirtualNetworkNodeEntityModel virtualNetworkNodeEntity;
            if (name is null)
            {
                virtualNetworkNodeEntity = await _virtualNetworkNodeRepository.CreateInvisible(virtualNetwork);
            }
            else
            {
                virtualNetworkNodeEntity = await _virtualNetworkNodeRepository.Create(name, virtualNetwork);
            }
            virtualNetworkNodeEntity.VirtualNetwork = virtualNetwork;

            return VirtualNetworkNodeEntityMapper.MapToDTO(virtualNetworkNodeEntity);
        }

        public async Task<List<VirtualNetworkNodeEntityDTO>> GetVisibleVirtualNetworkNodeEntities()
        {
            var virtualNetworkNodees = await _virtualNetworkNodeRepository.GetVisible();
            return [.. virtualNetworkNodees.Select(VirtualNetworkNodeEntityMapper.MapToDTO)];
        }

        public async Task<VirtualNetworkNodeEntityDTO> UpdateVirtualNetworkNodeEntityPosition(int entityId, int x, int y)
        {
            var model = await _virtualNetworkNodeRepository.UpdateEntityPosition(entityId, x, y);

            return VirtualNetworkNodeEntityMapper.MapToDTO(model);
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

        public async Task<VirtualNetworkModel> CreateNodeVirtualNetwork()
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

        public Task<VirtualNetworkModel> GetVirtualNetworkUsingBridgeName(string bridgeName)
        {
            return _networkRepository.GetByBridgeName(bridgeName);
        }
    }
}
