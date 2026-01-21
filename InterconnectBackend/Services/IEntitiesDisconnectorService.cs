using Models.DTO;

namespace Services
{
    /// <summary>
    /// Service responsible for disconnecting network entities.
    /// </summary>
    public interface IEntitiesDisconnectorService
    {
        /// <summary>
        /// Disconnects entities based on connection identifier.
        /// </summary>
        /// <param name="connectionId">Connection identifier to remove.</param>
        /// <returns>Removed connection data.</returns>
        Task<VirtualNetworkConnectionDTO> DisconnectEntities(int connectionId);
        
        /// <summary>
        /// Disconnects a virtual machine from a virtual network node.
        /// </summary>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="sourceEntityId">Virtual machine identifier.</param>
        /// <param name="destinationEntityId">Network node identifier.</param>
        Task DisconnectVirtualMachineFromVirtualNetworkNode(int connectionId, int sourceEntityId, int destinationEntityId);
        
        /// <summary>
        /// Disconnects two virtual machines.
        /// </summary>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="sourceEntityId">First virtual machine identifier.</param>
        /// <param name="destinationEntityId">Second virtual machine identifier.</param>
        Task DisconnectVirtualMachineFromVirtualMachine(int connectionId, int sourceEntityId, int destinationEntityId);
        
        /// <summary>
        /// Disconnects a virtual machine from the Internet.
        /// </summary>
        /// <param name="connectionId">Connection identifier.</param>
        /// <param name="virtualMachineEntityId">Virtual machine identifier.</param>
        /// <param name="internetEntityId">Internet entity identifier.</param>
        Task DisconnectVirtualMachineFromInternet(int connectionId, int virtualMachineEntityId, int internetEntityId);
    }
}
