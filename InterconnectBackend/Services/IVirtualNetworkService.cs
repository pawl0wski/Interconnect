using Models;

namespace Services
{
    public interface IVirtualNetworkService
    {
        void CreateVirtualNetwork(VirtualNetworkCreateDefinition definition);
    }
}
