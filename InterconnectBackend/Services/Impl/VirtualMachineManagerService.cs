using Mappers;
using Microsoft.Extensions.Options;
using Models;
using Models.Config;
using NativeLibrary.Wrappers;
using Services.Utils;

namespace Services.Impl
{
    /// <summary>
    /// Service managing virtual machines in the hypervisor.
    /// </summary>
    public class VirtualMachineManagerService : IVirtualMachineManagerService
    {
        private readonly IVirtualizationWrapper _vmManager;
        private readonly IVirtualMachineConsoleService _vmConsoleService;
        private readonly InterconnectConfig _config;

        /// <summary>
        /// Initializes a new instance of the VirtualMachineManagerService.
        /// </summary>
        /// <param name="vmManager">Virtualization wrapper for hypervisor operations.</param>
        /// <param name="config">Configuration options for interconnect.</param>
        /// <param name="vmConsoleService">Service for managing virtual machine consoles.</param>
        public VirtualMachineManagerService(
            IVirtualizationWrapper vmManager,
            IOptions<InterconnectConfig> config,
            IVirtualMachineConsoleService vmConsoleService)
        {
            _vmManager = vmManager;
            _config = config.Value;
            _vmConsoleService = vmConsoleService;
        }

        /// <summary>
        /// Creates a new virtual machine in the hypervisor.
        /// </summary>
        /// <param name="definition">Virtual machine definition to create.</param>
        /// <returns>Information about the created virtual machine.</returns>
        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition)
        {
            var name = definition.GetVirtualMachineNameWithPrefix(_config.VmPrefix);

            var builder = new VirtualMachineDefinitionBuilder();
            builder.SetPrefix(_config.VmPrefix).SetFromCreateDefinition(definition);

            var xmlDefinition = builder.Build();

            _vmManager.CreateVirtualMachine(xmlDefinition);
            var vmInfo = _vmManager.GetVirtualMachineInfo(name);

            _vmConsoleService.OpenVirtualMachineConsole(Guid.Parse(vmInfo.Uuid));

            return NativeVirtualMachineInfoMapper.MapToVirtualMachineInfo(vmInfo);
        }

        /// <summary>
        /// Retrieves a list of all virtual machines from the hypervisor.
        /// </summary>
        /// <returns>List of virtual machines.</returns>
        public List<VirtualMachineInfo> GetListOfVirtualMachines()
        {
            var virtualMachines = _vmManager.GetListOfVirtualMachines();

            return [.. virtualMachines.Select(NativeVirtualMachineInfoMapper.MapToVirtualMachineInfo)];
        }

        /// <summary>
        /// Attaches a network interface to a virtual machine.
        /// </summary>
        /// <param name="name">Virtual machine name.</param>
        /// <param name="networkName">Virtual network name.</param>
        /// <param name="macAddress">Interface MAC address.</param>
        public void AttachVirtualNetworkInterfaceToVirtualMachine(string name, string networkName, string macAddress)
        {
            var virtualMachine = _vmManager.GetVirtualMachineInfo(name);

            var createDefinition = new VirtualNetworkInterfaceCreateDefinition
            {
                MacAddress = macAddress,
                NetworkName = networkName,
            };
            var builder = new VirtualNetworkInterfaceCreateDefinitionBuilder()
                .SetFromCreateDefinition(createDefinition);

            var definition = builder.Build();

            _vmManager.AttachDeviceToVirtualMachine(Guid.Parse(virtualMachine.Uuid), definition);
        }
    }
}
