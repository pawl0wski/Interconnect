using Models.DTO;
using Models.Enums;

namespace Services
{
    /// <summary>
    /// Service managing virtual machine entities.
    /// </summary>
    public interface IVirtualMachineEntityService
    {
        /// <summary>
        /// Retrieves a list of all virtual machine entities.
        /// </summary>
        /// <returns>List of virtual machine entities.</returns>
        public Task<List<VirtualMachineEntityDTO>> GetEntities();
        
        /// <summary>
        /// Retrieves a list of virtual machine MAC addresses.
        /// </summary>
        /// <returns>List of MAC addresses.</returns>
        public Task<List<VirtualMachineEntityMacAddressDTO>> GetMacAddresses();
        
        /// <summary>
        /// Creates a new virtual machine entity.
        /// </summary>
        /// <param name="name">Virtual machine name.</param>
        /// <param name="bootableDiskId">Bootable disk identifier.</param>
        /// <param name="memory">Amount of RAM in MB.</param>
        /// <param name="virtualCpus">Number of virtual CPUs.</param>
        /// <param name="type">Virtual machine entity type.</param>
        /// <param name="x">X coordinate position.</param>
        /// <param name="y">Y coordinate position.</param>
        /// <returns>Created virtual machine entity.</returns>
        public Task<VirtualMachineEntityDTO> CreateEntity(string name, int bootableDiskId, uint memory, uint virtualCpus, VirtualMachineEntityType type, int x, int y);
        
        /// <summary>
        /// Updates the position of a virtual machine entity on the board.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated entity.</returns>
        public Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y);
        
        /// <summary>
        /// Retrieves a virtual machine entity by identifier.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Virtual machine entity.</returns>
        public Task<VirtualMachineEntityDTO> GetById(int id);
        
        /// <summary>
        /// Updates the virtual machine UUID for an entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="uuid">New virtual machine UUID.</param>
        /// <returns>Updated entity.</returns>
        public Task<VirtualMachineEntityDTO> UpdateEntityVmUUID(int id, string uuid);
    }
}
