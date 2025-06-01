using Microsoft.Extensions.DependencyInjection;
using Services.Impl;

namespace Services
{
    static public class ServicesInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualMachineManagerService, VirtualMachineManagerService>();
            serviceCollection.AddSingleton<IInfoService, InfoService>();
        }
    }
}
