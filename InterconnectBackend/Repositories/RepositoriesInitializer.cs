using Microsoft.Extensions.DependencyInjection;
using Repositories.Impl;

namespace Repositories
{
    public static class RepositoriesInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualMachineConsoleDataRepository, VirtualMachineConsoleDataRepository>();
            serviceCollection.AddSingleton<IVirtualMachineConsoleStreamRepository, VirtualMachineConsoleStreamRepository>();

            serviceCollection.AddScoped<IBootableDiskRepository, BootableDiskRepository>();
            serviceCollection.AddScoped<IVirtualMachineEntityRepository, VirtualMachineEntityRepository>();
            serviceCollection.AddScoped<IVirtualNetworkConnectionRepository, VirtualNetworkConnectionRepository>();
            serviceCollection.AddScoped<IVirtualSwitchEntityRepository, VirtualSwitchEntityRepository>();
            serviceCollection.AddScoped<IInternetEntityRepository, InternetEntityRepository>();
            serviceCollection.AddScoped<IVirtualNetworkRepository, VirtualNetworkRepository>();
        }
    }
}
