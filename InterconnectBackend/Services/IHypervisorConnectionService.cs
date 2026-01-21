using Models;
using Models.Enums;

namespace Services
{
    /// <summary>
    /// Service managing connection to the virtualization hypervisor.
    /// </summary>
    public interface IHypervisorConnectionService
    {
        /// <summary>
        /// Initializes connection to the hypervisor.
        /// </summary>
        /// <param name="connectionUrl">Connection URL or null for default.</param>
        public void InitializeConnection(string? connectionUrl);
        
        /// <summary>
        /// Retrieves the current connection status with the hypervisor.
        /// </summary>
        /// <returns>Connection status.</returns>
        public ConnectionStatus GetConnectionStatus();
        
        /// <summary>
        /// Retrieves connection and hypervisor information.
        /// </summary>
        /// <returns>Connection information.</returns>
        public ConnectionInfo GetConnectionInfo();
    }
}
