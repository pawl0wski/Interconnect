using Models.DTO;

namespace Services
{
    public interface IVirtualNetworkNodeConnector
    {
        public Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualNetworkNodes(int sourceVirtualNetworkNode, int destinationVirtualNetworkNode);
        public Task DisconnectTwoVirtualNetworkNodes(int connectionId, int firstVirtualNetworkNodeId, int secondVirtualNetworkNode);
        public Task<VirtualNetworkConnectionDTO> ConnectVirtualNetworkNodeToInternet(int virtualNetworkNodeId, int internetId);
        public Task DisconnectVirtualNetworkNodeFromInternet(int connectionId, int virtualNetworkNodeId);
    }
}
