using Microsoft.Extensions.DependencyInjection;
using Repositories.Impl;

namespace Repositories
{
    /// <summary>
    /// Initializer for application repositories.
    /// </summary>
    public static class RepositoriesInitializer
    {
        /// <summary>
        /// Registers repositories in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualMachineConsoleDataRepository, VirtualMachineConsoleDataRepository>();
            serviceCollection.AddSingleton<IVirtualMachineConsoleStreamRepository, VirtualMachineConsoleStreamRepository>();
            serviceCollection.AddSingleton<IPacketSnifferRepository, PacketSnifferRepository>();

            serviceCollection.AddScoped<IBootableDiskRepository, BootableDiskRepository>();
            serviceCollection.AddScoped<IVirtualMachineEntityRepository, VirtualMachineEntityRepository>();
            serviceCollection.AddScoped<IVirtualNetworkConnectionRepository, VirtualNetworkConnectionRepository>();
            serviceCollection.AddScoped<IVirtualNetworkNodeEntityRepository, VirtualNetworkNodeEntityRepository>();
            serviceCollection.AddScoped<IInternetEntityRepository, InternetEntityRepository>();
            serviceCollection.AddScoped<IVirtualNetworkRepository, VirtualNetworkRepository>();
            serviceCollection.AddScoped<IVirtualMachineEntityNetworkInterfaceRepository, VirtualMachineEntityNetworkInterfaceRepository>();
        }
    }
}
