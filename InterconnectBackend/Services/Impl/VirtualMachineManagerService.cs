using Mappers;
using Models;
using Models.Mappers;
using NativeLibrary.Wrappers;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualMachineManagerService : IVirtualMachineManagerService
    {
        private readonly IVirtualMachineManagerWrapper _vmManager;

        public VirtualMachineManagerService(IVirtualMachineManagerWrapper vmManager) => _vmManager = vmManager;
        
        public void InitializeConnection(string? connectionUrl)
        {
            _vmManager.InitializeConnection(connectionUrl ?? "qemu:///session");
        }

        public ConnectionInfo GetConnectionInfo()
        {
            var nativeConnectionInfo = _vmManager.GetConnectionInfo();

            return NativeConnectionInfoToConnectionInfoMapper.Map(nativeConnectionInfo);
        }

        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition)
        {
            var builder = new VirtualMachineCreateDefinitionBuilder();
            builder.SetFromCreateDefinition(definition);

            var xmlDefinition = builder.Build();

            _vmManager.CreateVirtualMachine(xmlDefinition);
            var vmInfo = _vmManager.GetVirtualMachineInfo(definition.Name);

            return NativeVirtualMachineInfoToVirtualMachineInfo.Map(vmInfo);
        }
    }
}
