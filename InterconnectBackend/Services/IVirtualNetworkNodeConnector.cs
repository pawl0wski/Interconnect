using Models.DTO;

namespace Services
{
    /// <summary>
    /// Service responsible for connecting virtual network nodes.
    /// </summary>
    public interface IVirtualNetworkNodeConnector
    {
        /// <summary>
        /// Connects two virtual network nodes.
        /// </summary>
        /// <param name="sourceVirtualNetworkNode">First node identifier.</param>
        /// <param name="destinationVirtualNetworkNode">Second node identifier.</param>
        /// <returns>Created connection data.</returns>
        public Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualNetworkNodes(int sourceVirtualNetworkNode, int destinationVirtualNetworkNode);
        
        /// <summary>
        /// Disconnects two virtual network nodes.
        /// </summary>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="firstVirtualNetworkNodeId">First node identifier.</param>
        /// <param name="secondVirtualNetworkNode">Second node identifier.</param>
        public Task DisconnectTwoVirtualNetworkNodes(int connectionId, int firstVirtualNetworkNodeId, int secondVirtualNetworkNode);
        
        /// <summary>
        /// Connects a virtual network node to the Internet.
        /// </summary>
        /// <param name="virtualNetworkNodeId">Network node identifier.</param>
        /// <param name="internetId">Internet entity identifier.</param>
        /// <returns>Created connection data.</returns>
        public Task<VirtualNetworkConnectionDTO> ConnectVirtualNetworkNodeToInternet(int virtualNetworkNodeId, int internetId);
        
        /// <summary>
        /// Disconnects a virtual network node from the Internet.
        /// </summary>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="virtualNetworkNodeId">Network node identifier.</param>
        public Task DisconnectVirtualNetworkNodeFromInternet(int connectionId, int virtualNetworkNodeId);
    }
}
