using Models;
using Models.Enums;

namespace Services
{
    public interface IHypervisorConnectionService
    {
        public void InitializeConnection(string? connectionUrl);
        public ConnectionStatus GetConnectionStatus();
        public ConnectionInfo GetConnectionInfo();
    }
}
