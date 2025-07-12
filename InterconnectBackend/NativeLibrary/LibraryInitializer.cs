using NativeLibrary.Wrappers;
using NativeLibrary.Wrappers.Impl;
using Microsoft.Extensions.DependencyInjection;

namespace NativeLibrary
{
    public static class LibraryInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualMachineManagerWrapper, VirtualMachineManagerWrapper>();
        }
    }
}
