using Microsoft.Extensions.Hosting;

namespace Services
{
    /// <summary>
    /// Service responsible for application initialization during startup.
    /// </summary>
    internal interface IInitializationService : IHostedService
    {
        /// <summary>
        /// Initializes necessary application components.
        /// </summary>
        public void Initialize();
    }
}
