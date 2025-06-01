using Library.Models;

namespace Services
{
    public interface IVirtualMachineManagerService
    {
        public void InitializeConnection();
        public ConnectionInfo GetConnectionInfo();
    }
}
