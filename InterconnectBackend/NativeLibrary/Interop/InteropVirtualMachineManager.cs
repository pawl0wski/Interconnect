using Library.Models;
using System.Runtime.InteropServices;

namespace Library.Interop
{
    public class InteropVirtualMachineManager
    {
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr VirtualMachineManager_Create();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_Destroy();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_InitializeConnection(ref ExecutionInfo exception, IntPtr manager, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetConnectionInfo(ref ExecutionInfo exception, IntPtr manager, ref ConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern nint VirtualMachineManager_CreateVirtualMachine(string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern nint VirtualMachineManager_GetInfoAboutVirtualMachine(string uuid);
    }
}
