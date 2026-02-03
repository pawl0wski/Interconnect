
using Microsoft.Extensions.Options;
using Models.Config;

namespace Services.Impl
{
    /// <summary>
    /// Service responsible for application initialization during startup.
    /// </summary>
    public class InitializationService : IInitializationService
    {
        private IHypervisorConnectionService _connService;
        private InterconnectConfig _config;

        /// <summary>
        /// Initializes a new instance of the InitializationService.
        /// </summary>
        /// <param name="connService">Service for managing hypervisor connection.</param>
        /// <param name="config">Configuration options for interconnect.</param>
        public InitializationService(IHypervisorConnectionService connService, IOptions<InterconnectConfig> config)
        {
            _connService = connService;
            _config = config.Value;
        }

        /// <summary>
        /// Initializes necessary application components.
        /// </summary>
        public void Initialize()
        {
            _connService.InitializeConnection(_config.HypervisorUrl);
        }

        /// <summary>
        /// Starts the initialization service asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>A task representing the asynchronous start operation.</returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Initialize();
            return Task.CompletedTask;
        }

        /// <summary>
        /// Stops the initialization service asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token for the operation.</param>
        /// <returns>A task representing the asynchronous stop operation.</returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
