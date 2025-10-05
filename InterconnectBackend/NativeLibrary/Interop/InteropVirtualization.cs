using NativeLibrary.Structs;
using System.Runtime.InteropServices;

namespace NativeLibrary.Interop
{
    internal static class InteropVirtualization
    {
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr CreateVirtualizationFacade();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void DestroyVirtualizationFacade();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_InitializeConnection(out NativeExecutionInfo executionInfo, IntPtr facade, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_GetConnectionInfo(out NativeExecutionInfo executionInfo, IntPtr facade, out NativeConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_CreateVirtualMachine(out NativeExecutionInfo executionInfom, IntPtr facade, string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_GetInfoAboutVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string name, out NativeVirtualMachineInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_GetNumberOfVirtualMachines(out NativeExecutionInfo executionInfo, IntPtr facade, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_GetListOfVirtualMachinesWithInfo(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr arrayOfVirtualMachines, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_IsConnectionAlive(out NativeExecutionInfo executionInfo, IntPtr facade, out bool isConnectionAlive);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_OpenVirtualMachineConsole(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr stream, string vmUuid);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_ReceiveDataFromConsole(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream, out NativeStreamData streamData);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_SendDataToConsole(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream, string buffer);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_CloseStream(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_CreateVirtualNetwork(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr network, string networkDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_AttachDeviceToVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_DetachDeviceFromVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_UpdateVmDevice(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Virtualization_DestroyNetwork(out NativeExecutionInfo executionInfo, IntPtr facade, string name);
    }
}
