using BackgroundServices.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace BackgroundServices
{
    public static class BackgroundServicesInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddHostedService<VirtualMachineConsoleBackgroundService>();
            serviceCollection.AddHostedService<PacketSnifferBackgroundService>();
        }
    }
}
