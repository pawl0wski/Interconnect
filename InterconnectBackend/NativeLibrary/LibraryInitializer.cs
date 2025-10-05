using Microsoft.Extensions.DependencyInjection;
using NativeLibrary.Wrappers;
using NativeLibrary.Wrappers.Impl;

namespace NativeLibrary
{
    public static class LibraryInitializer
    {
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualizationWrapper, VirtualizationWrapper>();
            serviceCollection.AddSingleton<IPacketSnifferWrapper, PacketSnifferWrapper>();
        }
    }
}
