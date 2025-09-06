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

        public VirtualNetworkService(
            IVirtualizationWrapper wrapper,
            IVirtualMachineEntityService vmEntityService,
            IVirtualNetworkEntityConnectionRepository connectionRepository)
        {
            _wrapper = wrapper;
            _vmEntityService = vmEntityService;
            _connectionRepository = connectionRepository;
        }

        public async Task<VirtualNetworkEntityConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int sourceSocketId, int destinationEntityId, int destinationSocketId)
        {
            var networkName = $"InterconnectVMs-{Guid.NewGuid()}";
            var bridgeSuffixId = networkName.Split("-").Last();
            var bridgeName = $"lv{bridgeSuffixId}";
            var sourceEntity = await _vmEntityService.GetEntityById(sourceEntityId);
            var destinationEntity = await _vmEntityService.GetEntityById(destinationEntityId);

            CreateVirtualNetwork(new VirtualNetworkCreateDefinition { NetworkName = networkName, BridgeName = bridgeName });

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

            await _connectionRepository.CreateNew(bridgeName, sourceEntity.VmUuid.Value, destinationEntity.VmUuid.Value);

            return new VirtualNetworkEntityConnectionDTO
            {
                FirstEntityUuid = sourceEntity.VmUuid.Value,
                SecondEntityUuid = destinationEntity.VmUuid.Value,
            };
        }

        public async Task<List<VirtualNetworkEntityConnectionDTO>> GetVirtualNetworkConnections()
        {
            var connections = await _connectionRepository.GetAll();
            return [.. connections.Select(VirtualNetworkEntityConnectionMapper.MapToDTO)];
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
    }
}
