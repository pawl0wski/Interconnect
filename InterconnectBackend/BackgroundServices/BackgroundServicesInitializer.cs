using BackgroundServices.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundServices
{
    /// <summary>
    /// Initializer for background services.
    /// </summary>
    public static class BackgroundServicesInitializer
    {
        /// <summary>
        /// Registers background services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">Services collection.</param>
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<VirtualMachineConsoleBackgroundService>();
            serviceCollection.AddHostedService<PacketSnifferBackgroundService>();
        }
    }
}
