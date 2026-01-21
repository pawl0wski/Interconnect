using Models.DTO;
using Models.Enums;

namespace Services
{
    /// <summary>
    /// Service responsible for connecting network entities.
    /// </summary>
    public interface IEntitiesConnectorService
    {
        /// <summary>
        /// Connects two entities of different types.
        /// </summary>
        /// <param name="sourceEntityId">Source entity identifier.</param>
        /// <param name="sourceEntityType">Source entity type.</param>
        /// <param name="destinationEntitiyId">Destination entity identifier.</param>
        /// <param name="destinationEntityType">Destination entity type.</param>
        /// <returns>Created connection data.</returns>
        Task<VirtualNetworkConnectionDTO> ConnectTwoEntities(int sourceEntityId, EntityType sourceEntityType, int destinationEntitiyId, EntityType destinationEntityType);
        
        /// <summary>
        /// Connects two virtual machines.
        /// </summary>
        /// <param name="sourceEntityId">First virtual machine identifier.</param>
        /// <param name="destinationEntityId">Second virtual machine identifier.</param>
        /// <returns>Created connection data.</returns>
        Task<VirtualNetworkConnectionDTO> ConnectTwoVirtualMachines(int sourceEntityId, int destinationEntityId);
        
        /// <summary>
        /// Connects a virtual machine to a virtual network node.
        /// </summary>
        /// <param name="sourceEntityId">Virtual machine identifier.</param>
        /// <param name="destinationEntityId">Network node identifier.</param>
        /// <returns>Created connection data.</returns>
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToVirtualNetworkNode(int sourceEntityId, int destinationEntityId);
        
        /// <summary>
        /// Connects a virtual machine to the Internet.
        /// </summary>
        /// <param name="sourceEntityId">Virtual machine identifier.</param>
        /// <param name="destinationEntityId">Internet entity identifier.</param>
        /// <returns>Created connection data.</returns>
        Task<VirtualNetworkConnectionDTO> ConnectVirtualMachineToInternet(int sourceEntityId, int destinationEntityId);
    }
}
