using NativeLibrary.Structs;

namespace NativeLibrary.Wrappers
{
    public interface IVirtualMachineManagerWrapper
    {
        public void InitializeConnection(string? connectionUrl);
        public NativeConnectionInfo GetConnectionInfo();
    }
}
