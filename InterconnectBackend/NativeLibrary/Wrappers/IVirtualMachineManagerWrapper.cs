using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers
{
    public interface IVirtualMachineManagerWrapper
    {
        public void InitializeConnection(string? connectionUrl);
        public NativeConnectionInfo GetConnectionInfo();
        public void CreateVirtualMachine(string xmlDefinition);
        public NativeVirtualMachineInfo GetVirtualMachineInfo(string name);
    }
}
