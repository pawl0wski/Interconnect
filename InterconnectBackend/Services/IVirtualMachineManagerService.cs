using Models;

namespace Services
{
    public interface IVirtualMachineManagerService
    {
        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition);
        public List<VirtualMachineInfo> GetListOfVirtualMachines();
        public void AttachVirtualNetworkInterfaceToVirtualMachine(string name, string networkName, string macAddress);
    }
}
