using Models;
using NativeLibrary.Wrappers;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualNetworkService : IVirtualNetworkService
    {
        private readonly IVirtualizationWrapper _wrapper;
        private readonly IVirtualMachineEntityService _vmEntityService;

        public VirtualNetworkService(IVirtualizationWrapper wrapper, IVirtualMachineEntityService vmEntityService)
        {
            _wrapper = wrapper;
            _vmEntityService = vmEntityService;
        }

        public void CreateVirtualNetwork(VirtualNetworkCreateDefinition definition)
        {
            var builder = new VirtualNetworkCreateDefinitionBuilder().SetFromCreateDefinition(definition);
            var networkDefinition = builder.Build();

            _wrapper.CreateVirtualNetwork(networkDefinition);
        }

        public async Task ConnectTwoVirtualMachines(int sourceEntityId, int sourceSocketId, int destinationEntityId, int destinationSocketId)
        {
            var sourceEntity = await _vmEntityService.GetEntityById(sourceEntityId);
            var destinationEntity = await _vmEntityService.GetEntityById(destinationEntityId);

            CreateVirtualNetwork(new VirtualNetworkCreateDefinition { NetworkName = "Test" });

            if (sourceEntity.VmUuid is null || destinationEntity.VmUuid is null)
            {
                throw new NullReferenceException("Source or destination entity vmuuid is null");
            }

            AttachNetworkInterfaceToVirtualMachine(sourceEntity.VmUuid.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = "Test",
                MacAddress = "1d:4c:24:4d:f0:c6"
            });
            AttachNetworkInterfaceToVirtualMachine(destinationEntity.VmUuid.Value, new VirtualNetworkInterfaceCreateDefinition
            {
                NetworkName = "Test",
                MacAddress = "58:cd:0c:de:67:94"
            });
        }

        public void AttachNetworkInterfaceToVirtualMachine(Guid uuid, VirtualNetworkInterfaceCreateDefinition interfaceDefinition)
        {
            var interfaceBuilder = new VirtualNetworkInterfaceCreateDefinitionBuilder();
            interfaceBuilder.SetFromCreateDefinition(interfaceDefinition);
            var xmlDefinition = interfaceBuilder.Build();

            _wrapper.AttachDeviceToVirtualMachine(uuid, xmlDefinition);
        }
    }
}
