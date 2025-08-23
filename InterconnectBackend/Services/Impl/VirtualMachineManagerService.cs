using Mappers;
using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using NativeLibrary.Wrappers;
using Services.Utils;

namespace Services.Impl
{
    public class VirtualMachineManagerService : IVirtualMachineManagerService
    {
        private readonly IVirtualizationWrapper _vmManager;
        private readonly IVirtualMachineConsoleService _vmConsoleService;
        private readonly InterconnectConfig _config;

        public VirtualMachineManagerService(IVirtualizationWrapper vmManager, IOptions<InterconnectConfig> config, IVirtualMachineConsoleService vmConsoleService)
        {
            _vmManager = vmManager;
            _config = config.Value;
            _vmConsoleService = vmConsoleService;
        }

        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition)
        {
            var name = definition.GetVirtualMachineNameWithPrefix(_config.VmPrefix);

            var builder = new VirtualMachineCreateDefinitionBuilder();
            builder.SetFromCreateDefinition(definition, _config.VmPrefix);

            var xmlDefinition = builder.Build();

            _vmManager.CreateVirtualMachine(xmlDefinition);
            var vmInfo = _vmManager.GetVirtualMachineInfo(name);

            _vmConsoleService.OpenVirtualMachineConsole(Guid.Parse(vmInfo.Uuid));

            return NativeVirtualMachineInfoMapper.MapToVirtualMachineInfo(vmInfo);
        }

        public List<VirtualMachineInfo> GetListOfVirtualMachines()
        {
            var virtualMachines = _vmManager.GetListOfVirtualMachines();

            return [.. virtualMachines.Select(NativeVirtualMachineInfoMapper.MapToVirtualMachineInfo)];
        }
    }
}
