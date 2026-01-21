using Models;

namespace Services
{
    /// <summary>
    /// Service managing virtual machines in the hypervisor.
    /// </summary>
    public interface IVirtualMachineManagerService
    {
        /// <summary>
        /// Creates a new virtual machine in the hypervisor.
        /// </summary>
        /// <param name="definition">Virtual machine definition to create.</param>
        /// <returns>Information about the created virtual machine.</returns>
        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition);
        
        /// <summary>
        /// Retrieves a list of all virtual machines from the hypervisor.
        /// </summary>
        /// <returns>List of virtual machines.</returns>
        public List<VirtualMachineInfo> GetListOfVirtualMachines();
        
        /// <summary>
        /// Attaches a network interface to a virtual machine.
        /// </summary>
        /// <param name="name">Virtual machine name.</param>
        /// <param name="networkName">Virtual network name.</param>
        /// <param name="macAddress">Interface MAC address.</param>
        public void AttachVirtualNetworkInterfaceToVirtualMachine(string name, string networkName, string macAddress);
    }
}
