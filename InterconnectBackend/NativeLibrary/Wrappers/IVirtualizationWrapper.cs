using NativeLibrary.Structs;
using NativeLibrary.Utils;

namespace NativeLibrary.Wrappers
{
    public interface IVirtualizationWrapper
    {
        public void InitializeConnection(string? connectionUrl);
        public NativeConnectionInfo GetConnectionInfo();
        public void CreateVirtualMachine(string xmlDefinition);
        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name);
        public INativeList<NativeVirtualMachineInfo> GetListOfVirtualMachines();
        public bool IsConnectionAlive();
        public IntPtr OpenVirtualMachineConsole(Guid uuid);
        public NativeStreamData GetDataFromStream(IntPtr stream);
        public void SendDataToStream(IntPtr stream, string data);
        public void CloseStream(IntPtr stream);
        public IntPtr CreateVirtualNetwork(string networkDefinition);
        public void AttachDeviceToVirtualMachine(Guid uuid, string deviceDefinition);
    }
}
