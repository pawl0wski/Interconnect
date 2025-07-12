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
        public void InitializeConnection(string? connectionUrl)
        {
            ExecutionInfo exception = new();

            InteropVirtualMachineManager.VirtualMachineManager_InitializeConnection(ref exception, _manager, connectionUrl);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(exception);
        }
        public ConnectionInfo GetConnectionInfo()
        {
            ExecutionInfo exception = new();
            ConnectionInfo info = new();

            InteropVirtualMachineManager.VirtualMachineManager_GetConnectionInfo(ref exception, _manager, ref info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(exception);
            return info;
        }

    }
}
