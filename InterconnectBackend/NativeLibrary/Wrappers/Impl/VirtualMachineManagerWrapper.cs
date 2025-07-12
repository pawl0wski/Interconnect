using NativeLibrary.Interop;
using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers.Impl
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
            NativeExecutionInfo exception = new();

            InteropVirtualMachineManager.VirtualMachineManager_InitializeConnection(ref exception, _manager, connectionUrl);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(exception);
        }
        public NativeConnectionInfo GetConnectionInfo()
        {
            NativeExecutionInfo exception = new();
            NativeConnectionInfo info = new();

            InteropVirtualMachineManager.VirtualMachineManager_GetConnectionInfo(ref exception, _manager, ref info);

            ExecutionInfoAnalyzer.ThrowIfErrorOccurred(exception);
            return info;
        }

    }
}
