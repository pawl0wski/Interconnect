using Models.Database;

namespace Repositories
{
    /// <summary>
    /// Repository managing bootable disks in the database.
    /// </summary>
    public interface IBootableDiskRepository
    {
        /// <summary>
        /// Retrieves a list of bootable disks with non-null paths.
        /// </summary>
        /// <returns>List of disks with paths.</returns>
        public Task<List<BootableDiskModel>> GetOnlyWithNotNullablePath();
        
        /// <summary>
        /// Retrieves a bootable disk by identifier.
        /// </summary>
        /// <param name="id">Disk identifier.</param>
        /// <returns>Bootable disk.</returns>
        public Task<BootableDiskModel> GetById(int id);
    }
}
