using Library.Interop;

namespace Library.Wrappers.Impl
{
    public class VirtualMachineManagerWrapper : IVirtualMachineManagerWrapper
    {
        IntPtr _manager = IntPtr.Zero;
        
        public VirtualMachineManagerWrapper()
        {
            _manager = LibraryVirtualMachineManager.VirtualMachineManager_Create();
        }

        public void InitializeConnection(string? connectionUrl)
        {
            LibraryVirtualMachineManager.VirtualMachineManager_InitializeConnection(_manager, connectionUrl);
        }
    }
}
