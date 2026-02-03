using Mappers;
using Models;
using Models.Enums;
using NativeLibrary.Wrappers;

namespace Services.Impl
{
    /// <summary>
    /// Service managing connection to the virtualization hypervisor.
    /// </summary>
    public class HypervisorConnectionService : IHypervisorConnectionService
    {
        private readonly IVirtualizationWrapper _vmManager;

        /// <summary>
        /// Initializes a new instance of the HypervisorConnectionService.
        /// </summary>
        /// <param name="vmManager">The virtualization wrapper for hypervisor operations.</param>
        public HypervisorConnectionService(IVirtualizationWrapper vmManager)
        {
            _vmManager = vmManager;
        }

        /// <summary>
        /// Initializes connection to the hypervisor.
        /// </summary>
        /// <param name="connectionUrl">Connection URL or null for default.</param>
        public void InitializeConnection(string? connectionUrl)
        {
            connectionUrl = String.IsNullOrEmpty(connectionUrl) ? "qemu:///session" : connectionUrl;

            _vmManager.InitializeConnection(connectionUrl);
        }

        /// <summary>
        /// Retrieves the current connection status with the hypervisor.
        /// </summary>
        /// <returns>Connection status.</returns>
        public ConnectionStatus GetConnectionStatus()
        {
            var status = _vmManager.IsConnectionAlive();

            return status ? ConnectionStatus.ALIVE : ConnectionStatus.DEAD;
        }

        /// <summary>
        /// Retrieves connection and hypervisor information.
        /// </summary>
        /// <returns>Connection information.</returns>
        public ConnectionInfo GetConnectionInfo()
        {
            var nativeConnectionInfo = _vmManager.GetConnectionInfo();

            return NativeConnectionInfoMapper.MapToConnectionInfo(nativeConnectionInfo);
        }
    }
}
