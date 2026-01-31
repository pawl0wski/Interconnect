using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing virtual machine entities in the database.
    /// </summary>
    public interface IVirtualMachineEntityRepository
    {
        /// <summary>
        /// Adds a new virtual machine entity.
        /// </summary>
        /// <param name="model">Entity model to add.</param>
        public Task Add(VirtualMachineEntityModel model);
        
        /// <summary>
        /// Retrieves all virtual machine entities.
        /// </summary>
        /// <returns>List of entities.</returns>
        public Task<List<VirtualMachineEntityModel>> GetAll();
        
        /// <summary>
        /// Retrieves a virtual machine entity by identifier.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Virtual machine entity.</returns>
        public Task<VirtualMachineEntityModel> GetById(int id);
        
        /// <summary>
        /// Updates a virtual machine entity.
        /// </summary>
        /// <param name="model">Entity model to update.</param>
        public Task Update(VirtualMachineEntityModel model);

        /// <summary>
        /// Removes the item with the specified identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the item to remove.</param>
        public Task Remove(int id);

    }
}
