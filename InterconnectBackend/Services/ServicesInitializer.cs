using Microsoft.Extensions.DependencyInjection;
using Repositories;
using Services.Impl;

namespace Services
{
    static public class ServicesInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHypervisorConnectionService, HypervisorConnectionService>();
            serviceCollection.AddSingleton<IVirtualMachineConsoleService, VirtualMachineConsoleService>();
            serviceCollection.AddSingleton<IVirtualMachineManagerService, VirtualMachineManagerService>();
            serviceCollection.AddSingleton<IInfoService, InfoService>();

            serviceCollection.AddScoped<IVirtualNetworkService, VirtualNetworkService>();
            serviceCollection.AddScoped<IVirtualMachineEntityService, VirtualMachineEntityService>();
            serviceCollection.AddScoped<IBootableDiskProviderService, BootableDiskProviderService>();
        }
    }
}
