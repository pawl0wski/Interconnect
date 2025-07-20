using Mappers;
using Models;
using Models.Enums;
using NativeLibrary.Wrappers;

namespace Services.Impl
{
    public class HypervisorConnectionService : IHypervisorConnectionService
    {
        private readonly IVirtualMachineManagerWrapper _vmManager;

        public HypervisorConnectionService(IVirtualMachineManagerWrapper vmManager)
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

            return NativeConnectionInfoToConnectionInfoMapper.Map(nativeConnectionInfo);
        }
    }
}
