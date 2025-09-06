using Mappers;
using Models;
using Models.DTO;
using NativeLibrary.Wrappers;
using Repositories;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualNetworkService : IVirtualNetworkService
    {
        private readonly IVirtualizationWrapper _wrapper;
        private readonly IVirtualMachineEntityService _vmEntityService;
        private readonly IVirtualNetworkEntityConnectionRepository _connectionRepository;
        private readonly IVirtualSwitchEntityRepository _switchRepository;

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityService vmEntityService,
            IVirtualNetworkEntityConnectionRepository connectionRepository,
            IVirtualSwitchEntityRepository switchRepository)
        {
            _wrapper = wrapper;
            _vmEntityService = vmEntityService;
            _connectionRepository = connectionRepository;
            _switchRepository = switchRepository;
        }

        public async Task<VirtualNetworkEntityConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int sourceSocketId, int destinationEntityId, int destinationSocketId)
        {
            var sourceEntity = await _vmEntityService.GetEntityById(sourceEntityId);
            var destinationEntity = await _vmEntityService.GetEntityById(destinationEntityId);

            var virtualSwitch = await CreateVirtualSwitch(null);
            var networkName = GetNetworkNameFromUuid(virtualSwitch.Uuid);

            if (sourceEntity.VmUuid is null || destinationEntity.VmUuid is null)
            {
                throw new NullReferenceException("Source or destination entity vmuuid is null");
            }

            AttachNetworkInterfaceToVirtualMachine(sourceEntity.VmUuid.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });
            AttachNetworkInterfaceToVirtualMachine(destinationEntity.VmUuid.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = networkName,
                MacAddress = MacAddressGenerator.Generate()
            });

            await _connectionRepository.Create(sourceEntity.VmUuid.Value, destinationEntity.VmUuid.Value);

            return new VirtualNetworkEntityConnectionDTO
            {
                Id = 1,
                FirstEntityUuid = sourceEntity.VmUuid.Value,
                SecondEntityUuid = destinationEntity.VmUuid.Value,
            };
        }

        public async Task<List<VirtualNetworkEntityConnectionDTO>> GetVirtualNetworkConnections()
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

            if (name is null)
            {
                await _switchRepository.CreateInvisible(bridgeName, networkUuid);
            }
            else
            {
                await _switchRepository.Create(name, bridgeName, networkUuid);
            }

            return new VirtualSwitchEntityDTO { BridgeName = bridgeName, Uuid = networkUuid };
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
    }
}
