using Microsoft.Extensions.DependencyInjection;
using Services.Impl;

namespace Services
{
    /// <summary>
    /// Initializer for application services.
    /// </summary>
    static public class ServicesInitializer
    {
        /// <summary>
        /// Registers application services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHypervisorConnectionService, HypervisorConnectionService>();
            serviceCollection.AddSingleton<IVirtualMachineConsoleService, VirtualMachineConsoleService>();
            serviceCollection.AddSingleton<IVirtualMachineManagerService, VirtualMachineManagerService>();
            serviceCollection.AddSingleton<IInfoService, InfoService>();
            serviceCollection.AddSingleton<IPacketSnifferService, PacketSnifferService>();

            serviceCollection.AddScoped<IVirtualNetworkService, VirtualNetworkService>();
            serviceCollection.AddScoped<IVirtualMachineEntityService, VirtualMachineEntityService>();
            serviceCollection.AddScoped<IBootableDiskProviderService, BootableDiskProviderService>();
            serviceCollection.AddScoped<IVirtualNetworkNodeConnector, VirtualNetworkNodeConnector>();
            serviceCollection.AddScoped<IInternetEntityService, InternetEntityService>();
            serviceCollection.AddScoped<IEntitiesDisconnectorService, EntitiesDisconnectorService>();
            serviceCollection.AddScoped<IEntitiesConnectorService, EntitiesConnectorService>();
        }
    }
}
