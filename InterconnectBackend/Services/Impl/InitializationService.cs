
using Microsoft.Extensions.Options;
using Models.Config;

namespace Services.Impl
{
    public class InitializationService : IInitializationService
    {
        private IHypervisorConnectionService _connService;
        private InterconnectConfig _config;

        public InitializationService(IHypervisorConnectionService connService, IOptions<InterconnectConfig> config)
        {
            _connService = connService;
            _config = config.Value;
        }

        public void Initialize()
        {
            _connService.InitializeConnection(_config.HypervisorUrl);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Initialize();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
