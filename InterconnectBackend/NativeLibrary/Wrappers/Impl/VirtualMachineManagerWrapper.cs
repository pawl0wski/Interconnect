using Library.Interop;
using Library.Models;

namespace Library.Wrappers.Impl
{
    public class VirtualMachineManagerWrapper : IVirtualMachineManagerWrapper
    {
        private IntPtr _manager = IntPtr.Zero;

        public VirtualMachineManagerWrapper()
        {
            _manager = InteropVirtualMachineManager.VirtualMachineManager_Create();
        }
        public void InitializeConnection(string? connectionUrl) => InteropVirtualMachineManager.VirtualMachineManager_InitializeConnection(_manager, connectionUrl);
        public ConnectionInfo GetConnectionInfo()
        {
            ConnectionInfo info = new();
            InteropVirtualMachineManager.VirtualMachineManager_GetConnectionInfo(_manager, ref info);
            return info;
        }

    }
}
