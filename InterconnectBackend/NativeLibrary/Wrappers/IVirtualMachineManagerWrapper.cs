
using Library.Models;

namespace Library.Wrappers
{
    public interface IVirtualMachineManagerWrapper
    {
        public void InitializeConnection(string? connectionUrl);
        public ConnectionInfo GetConnectionInfo();
    }
}
