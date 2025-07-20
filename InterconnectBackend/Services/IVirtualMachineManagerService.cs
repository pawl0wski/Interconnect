using Models;

namespace Services
{
    public interface IVirtualMachineManagerService
    {
        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition);
        public List<VirtualMachineInfo> GetListOfVirtualMachines();
    }
}
