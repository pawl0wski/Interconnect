using Models;
using Models.Mappers;
using NativeLibrary.Wrappers;

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
    }
}
