using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing virtual machine network interfaces in the database.
    /// </summary>
    public interface IVirtualMachineEntityNetworkInterfaceRepository
    {
        /// <summary>
        /// Creates a new network interface.
        /// </summary>
        /// <param name="networkInterface">Interface model to create.</param>
        public Task Create(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        
        /// <summary>
        /// Updates a network interface.
        /// </summary>
        /// <param name="networkInterface">Interface model to update.</param>
        public Task Update(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        
        /// <summary>
        /// Removes a network interface.
        /// </summary>
        /// <param name="networkInterface">Interface model to remove.</param>
        public Task Remove(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        
        /// <summary>
        /// Retrieves a network interface by virtual machine and connection identifiers.
        /// </summary>
        /// <param name="virtualMachineId">Virtual machine identifier.</param>
        /// <param name="connectionId">Connection identifier.</param>
        /// <returns>Network interface or null.</returns>
        public Task<VirtualMachineEntityNetworkInterfaceModel?> GetByIds(int virtualMachineId, int connectionId);
        
        /// <summary>
        /// Retrieves all network interfaces for a virtual machine.
        /// </summary>
        /// <param name="id">Virtual machine identifier.</param>
        /// <returns>List of network interfaces.</returns>
        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByVirtualMachineId(int id);
        
        /// <summary>
        /// Retrieves network interfaces connected to a specific network.
        /// </summary>
        /// <param name="virtualMachineId">Virtual machine identifier.</param>
        /// <param name="networkName">Network name.</param>
        /// <returns>List of network interfaces.</returns>
        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByNetworkName(int virtualMachineId, string networkName);
    }
}
