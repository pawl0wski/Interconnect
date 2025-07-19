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
        public static extern void VirtualMachineManager_InitializeConnection(ref NativeExecutionInfo executionInfo, IntPtr manager, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetConnectionInfo(ref NativeExecutionInfo executionInfo, IntPtr manager, ref NativeConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_CreateVirtualMachine(ref NativeExecutionInfo executionInfom, IntPtr manager, string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetInfoAboutVirtualMachine(ref NativeExecutionInfo executionInfo, IntPtr manager, string name, ref NativeVirtualMachineInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetNumberOfVirtualMachines(in NativeExecutionInfo executionInfo, IntPtr manager, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void VirtualMachineManager_GetListOfVirtualMachinesWithInfo(in NativeExecutionInfo executionInfo, IntPtr manager, out IntPtr arrayOfVirtualMachines, out int numberOfVirtualMachines);
    }
}
