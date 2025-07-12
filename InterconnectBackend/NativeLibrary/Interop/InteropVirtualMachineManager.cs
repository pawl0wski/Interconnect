using NativeLibrary.Structs;
using System.Runtime.InteropServices;

namespace NativeLibrary.Interop
{
    public class InteropVirtualMachineManager
    {
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr VirtualMachineManager_Create();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_Destroy();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_InitializeConnection(ref NativeExecutionInfo exception, IntPtr manager, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetConnectionInfo(ref NativeExecutionInfo exception, IntPtr manager, ref NativeConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern nint VirtualMachineManager_CreateVirtualMachine(string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern nint VirtualMachineManager_GetInfoAboutVirtualMachine(string uuid);
    }
}
