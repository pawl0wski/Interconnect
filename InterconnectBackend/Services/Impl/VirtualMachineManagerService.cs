using Mappers;
using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using NativeLibrary.Structs;
using NativeLibrary.Wrappers;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualMachineManagerService : IVirtualMachineManagerService
    {
        private readonly IVirtualMachineManagerWrapper _vmManager;
        private readonly InterconnectConfig _config;

        public VirtualMachineManagerService(IVirtualMachineManagerWrapper vmManager, IOptions<InterconnectConfig> config)
        {
            _vmManager = vmManager;
            _config = config.Value;
        }

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
            var name = definition.GetVirtualMachineNameWithPrefix(_config.VmPrefix);

            var builder = new VirtualMachineCreateDefinitionBuilder();
            builder.SetFromCreateDefinition(definition, _config.VmPrefix);

            var xmlDefinition = builder.Build();

            _vmManager.CreateVirtualMachine(xmlDefinition);
            var vmInfo = _vmManager.GetVirtualMachineInfo(name);

            return NativeVirtualMachineInfoToVirtualMachineInfo.Map(vmInfo);
        }

        public List<VirtualMachineInfo> GetListOfVirtualMachines()
        {
            var virtualMachines = _vmManager.GetListOfVirtualMachines();

            return [.. virtualMachines.Select(NativeVirtualMachineInfoToVirtualMachineInfo.Map)];
        }
    }
}
