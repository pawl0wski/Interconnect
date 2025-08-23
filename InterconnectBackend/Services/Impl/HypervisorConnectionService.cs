using Mappers;
using Models;
using Models.Enums;
using NativeLibrary.Wrappers;

namespace Services.Impl
{
    public class HypervisorConnectionService : IHypervisorConnectionService
    {
        private readonly IVirtualizationWrapper _vmManager;

        public HypervisorConnectionService(IVirtualizationWrapper vmManager)
        {
            _vmManager = vmManager;
        }

        public void InitializeConnection(string? connectionUrl)
        {
            connectionUrl = String.IsNullOrEmpty(connectionUrl) ? "qemu:///session" : connectionUrl;

            _vmManager.InitializeConnection(connectionUrl);
        }

        public ConnectionStatus GetConnectionStatus()
        {
            var status = _vmManager.IsConnectionAlive();

            return status ? ConnectionStatus.ALIVE : ConnectionStatus.DEAD;
        }

        public ConnectionInfo GetConnectionInfo()
        {
            var nativeConnectionInfo = _vmManager.GetConnectionInfo();

            return NativeConnectionInfoMapper.MapToConnectionInfo(nativeConnectionInfo);
        }
    }
}
