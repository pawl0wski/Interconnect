using Models;

namespace Services
{
    public interface IVirtualMachineManagerService
    {
        public void InitializeConnection(string? connectionUrl);
        public ConnectionInfo GetConnectionInfo();
        public VirtualMachineInfo CreateVirtualMachine(VirtualMachineCreateDefinition definition);
        public List<VirtualMachineInfo> GetListOfVirtualMachines();
    }
}
