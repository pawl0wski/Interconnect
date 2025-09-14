using NativeLibrary.Structs;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;

namespace NativeLibrary.Interop
{
    public class InteropVirtualization
    {
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern IntPtr Create();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void Destroy();
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void InitializeConnection(out NativeExecutionInfo executionInfo, IntPtr facade, string? customConnectionUrl);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void GetConnectionInfo(out NativeExecutionInfo executionInfo, IntPtr facade, out NativeConnectionInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void CreateVirtualMachine(out NativeExecutionInfo executionInfom, IntPtr facade, string virtualMachineXml);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void GetInfoAboutVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string name, out NativeVirtualMachineInfo info);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void GetNumberOfVirtualMachines(out NativeExecutionInfo executionInfo, IntPtr facade, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void GetListOfVirtualMachinesWithInfo(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr arrayOfVirtualMachines, out int numberOfVirtualMachines);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void IsConnectionAlive(out NativeExecutionInfo executionInfo, IntPtr facade, out bool isConnectionAlive);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void OpenVirtualMachineConsole(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr stream, string vmUuid);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void ReceiveDataFromConsole(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream, out NativeStreamData streamData);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void SendDataToConsole(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream, string buffer);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void CloseStream(out NativeExecutionInfo executionInfo, IntPtr facade, IntPtr stream);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void CreateVirtualNetwork(out NativeExecutionInfo executionInfo, IntPtr facade, out IntPtr network, string networkDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void AttachDeviceToVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void DetachDeviceFromVirtualMachine(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void UpdateVmDevice(out NativeExecutionInfo executionInfo, IntPtr facade, string uuid, string deviceDefinition);
        [DllImport(Constants.LIBRARY_NAME)]
        public static extern void DestroyNetwork(out NativeExecutionInfo executionInfo, IntPtr facade, string name);
    }
}
