using Mappers;
using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using Models.Database;
using Models.DTO;
using NativeLibrary.Wrappers;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    /// <summary>
    /// Service managing virtual networks.
    /// </summary>
    public class VirtualNetworkService : IVirtualNetworkService
    {
        private readonly InterconnectConfig _config;
        private readonly IVirtualizationWrapper _wrapper;
        private readonly IVirtualMachineEntityRepository _vmEntityRepository;
        private readonly IVirtualNetworkConnectionRepository _connectionRepository;
        private readonly IVirtualNetworkNodeEntityRepository _virtualNetworkNodeRepository;
        private readonly IVirtualNetworkRepository _networkRepository;
        private readonly IPacketSnifferService _packetSnifferService;
        private readonly IVirtualMachineEntityNetworkInterfaceRepository _networkInterfaceRepository;

        /// <summary>
        /// Initializes a new instance of the VirtualNetworkService.
        /// </summary>
        /// <param name="config">Configuration options for interconnect.</param>
        /// <param name="wrapper">Virtualization wrapper for hypervisor operations.</param>
        /// <param name="vmEntityRepository">Repository for virtual machine entities.</param>
        /// <param name="connectionRepository">Repository for network connections.</param>
        /// <param name="virtualNetworkNodeRepository">Repository for network node entities.</param>
        /// <param name="networkRepository">Repository for virtual networks.</param>
        /// <param name="packetSnifferService">Service for packet sniffing.</param>
        /// <param name="networkInterfaceRepository">Repository for network interfaces.</param>
        public VirtualNetworkService(
            IOptions<InterconnectConfig> config,
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityRepository vmEntityRepository,
            IVirtualNetworkConnectionRepository connectionRepository,
            IVirtualNetworkNodeEntityRepository virtualNetworkNodeRepository,
            IVirtualNetworkRepository networkRepository,
            IPacketSnifferService packetSnifferService,
            IVirtualMachineEntityNetworkInterfaceRepository networkInterfaceRepository)
        {
            _config = config.Value;
            _wrapper = wrapper;
            _vmEntityRepository = vmEntityRepository;
            _connectionRepository = connectionRepository;
            _virtualNetworkNodeRepository = virtualNetworkNodeRepository;
            _networkRepository = networkRepository;
            _packetSnifferService = packetSnifferService;
            _networkInterfaceRepository = networkInterfaceRepository;
        }

        /// <summary>
        /// Retrieves a list of all connections in the virtual network.
        /// </summary>
        /// <returns>List of connections.</returns>
        public async Task<List<VirtualNetworkConnectionDTO>> GetVirtualNetworkConnections()
        {
            var connections = await _connectionRepository.GetAll();
            return [.. connections.Select(VirtualNetworkEntityConnectionMapper.MapToDTO)];
        }

        /// <summary>
        /// Creates a new virtual network node.
        /// </summary>
        /// <param name="name">Node name.</param>
        /// <returns>Created network node.</returns>
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

        /// <summary>
        /// Retrieves a list of visible virtual network nodes.
        /// </summary>
        /// <returns>List of visible nodes.</returns>
        public async Task<List<VirtualNetworkNodeEntityDTO>> GetVisibleVirtualNetworkNodeEntities()
        {
            var virtualNetworkNodees = await _virtualNetworkNodeRepository.GetVisible();
            return [.. virtualNetworkNodees.Select(VirtualNetworkNodeEntityMapper.MapToDTO)];
        }

        /// <summary>
        /// Updates the position of a virtual network node.
        /// </summary>
        /// <param name="entityId">Node identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated node.</returns>
        public async Task<VirtualNetworkNodeEntityDTO> UpdateVirtualNetworkNodeEntityPosition(int entityId, int x, int y)
        {
            var model = await _virtualNetworkNodeRepository.UpdateEntityPosition(entityId, x, y);

            return VirtualNetworkNodeEntityMapper.MapToDTO(model);
        }

        /// <summary>
        /// Attaches a network interface to a virtual machine.
        /// </summary>
        /// <param name="vmId">Virtual machine identifier.</param>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="interfaceDefinition">Network interface definition.</param>
        public async Task AttachNetworkInterfaceToVirtualMachine(int vmId, int connectionId, VirtualNetworkInterfaceCreateDefinition interfaceDefinition)
        {
            var virtualMachine = await _vmEntityRepository.GetById(vmId);
            var interfaceBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            interfaceBuilder.SetFromCreateDefinition(interfaceDefinition);
            var xmlDefinition = interfaceBuilder.Build();

            _wrapper.AttachDeviceToVirtualMachine(virtualMachine.VmUuid!.Value, xmlDefinition);

            var networkInterfaceModel = new VirtualMachineEntityNetworkInterfaceModel
            {
                Definition = xmlDefinition,
                VirtualMachineEntityId = virtualMachine.Id,
                VirtualNetworkEntityConnectionId = connectionId
            };
            await _networkInterfaceRepository.Create(networkInterfaceModel);
        }

        /// <summary>
        /// Updates the network for a virtual machine network interface.
        /// </summary>
        /// <param name="vmId">Virtual machine identifier.</param>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="networkName">New network name.</param>
        public async Task UpdateNetworkForVirtualMachineNetworkInterface(int vmId, int connectionId, string networkName)
        {
            var vmInterface = await _networkInterfaceRepository.GetByIds(vmId, connectionId);
            var vmEntity = await _vmEntityRepository.GetById(vmId);

            if (vmInterface is null)
            {
                throw new NullReferenceException("Virtual machine network interface not found");
            }

            if (vmEntity.VmUuid is null)
            {
                throw new NullReferenceException("Virtual machine UUID is null");
            }

            var builder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            builder.SetFromXml(vmInterface.Definition).SetNetworkName(networkName);
            var deviceDefinition = builder.Build();

            _wrapper.UpdateVmDevice(vmEntity.VmUuid!.Value, deviceDefinition);

            vmInterface.Definition = deviceDefinition;
            await _networkInterfaceRepository.Update(vmInterface);
        }

        /// <summary>
        /// Creates a virtual network for Internet connection.
        /// </summary>
        /// <returns>Created virtual network.</returns>
        public async Task<VirtualNetworkModel> CreateInternetVirtualNetwork()
        {
            var (networkName, bridgeName) = CreateNetworkAndBridgeNames();

            return await CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName,
                ForwardNat = true,
                IpAddress = _config.InternetEntityDefaultIp,
                NetMask = _config.InternetEntityDefaultNetmask
            });
        }

        /// <summary>
        /// Creates a virtual network for a network node.
        /// </summary>
        /// <returns>Created virtual network.</returns>
        public async Task<VirtualNetworkModel> CreateNodeVirtualNetwork()
        {
            var (networkName, bridgeName) = CreateNetworkAndBridgeNames();

            return await CreateVirtualNetwork(new VirtualNetworkCreateDefinition
            {
                NetworkName = networkName,
                BridgeName = bridgeName
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

        /// <summary>
        /// Retrieves all virtual networks.
        /// </summary>
        /// <returns>List of virtual networks.</returns>
        public Task<List<VirtualNetworkModel>> GetAllVirtualNetworks()
        {
            return _networkRepository.GetAll();
        }

        /// <summary>
        /// Retrieves a virtual network based on bridge name.
        /// </summary>
        /// <param name="bridgeName">Network bridge name.</param>
        /// <returns>Virtual network.</returns>
        public Task<VirtualNetworkModel> GetVirtualNetworkUsingBridgeName(string bridgeName)
        {
            return _networkRepository.GetByBridgeName(bridgeName);
        }
    }
}
