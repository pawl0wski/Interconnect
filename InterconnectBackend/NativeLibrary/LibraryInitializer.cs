using Microsoft.Extensions.DependencyInjection;
using NativeLibrary.Wrappers;
using NativeLibrary.Wrappers.Impl;

namespace NativeLibrary
{
    /// <summary>
    /// Initializer for native virtualization library.
    /// </summary>
    public static class LibraryInitializer
    {
        /// <summary>
        /// Initializes native library services in the dependency injection container.
        /// </summary>
        /// <param name="serviceCollection">The service collection.</param>
        public static void Initialize(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IVirtualizationWrapper, VirtualizationWrapper>();
            serviceCollection.AddSingleton<IPacketSnifferWrapper, PacketSnifferWrapper>();
        }
    }
}
