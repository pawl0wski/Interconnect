using Library.Wrappers;
using Library.Wrappers.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace Library
{
    public static class LibraryInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualMachineManagerWrapper, VirtualMachineManagerWrapper>();
        }
    }
}
