using Library.Models;

namespace Services
{
    public interface IVirtualMachineManagerService
    {
        public void InitializeConnection(string? connectionUrl);
        public ConnectionInfo GetConnectionInfo();
    }
}
