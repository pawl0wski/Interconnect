using Library.Models;
using Library.Wrappers;

namespace Services.Impl
{
    public class VirtualMachineManagerService : IVirtualMachineManagerService
    {
        private readonly IVirtualMachineManagerWrapper _vmManager;

        public VirtualMachineManagerService(IVirtualMachineManagerWrapper vmManager) => _vmManager = vmManager;
        public void InitializeConnection()
            => _vmManager.InitializeConnection(null);
        public ConnectionInfo GetConnectionInfo()
            => _vmManager.GetConnectionInfo();
    }
}
