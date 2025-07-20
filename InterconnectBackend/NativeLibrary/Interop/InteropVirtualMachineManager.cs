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
        public static extern void VirtualMachineManager_InitializeConnection(out NativeExecutionInfo executionInfo, IntPtr manager, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetConnectionInfo(out NativeExecutionInfo executionInfo, IntPtr manager, out NativeConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_CreateVirtualMachine(out NativeExecutionInfo executionInfom, IntPtr manager, string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetInfoAboutVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr manager, string name, out NativeVirtualMachineInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetNumberOfVirtualMachines(out NativeExecutionInfo executionInfo, IntPtr manager, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetListOfVirtualMachinesWithInfo(out NativeExecutionInfo executionInfo, IntPtr manager, out IntPtr arrayOfVirtualMachines, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_IsConnectionAlive(out NativeExecutionInfo executionInfo, IntPtr manager, out bool isConnectionAlive);
    }
}
