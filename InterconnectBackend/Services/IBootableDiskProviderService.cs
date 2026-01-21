using Models.DTO;

namespace Services
{
    /// <summary>
    /// Service providing information about available bootable disks.
    /// </summary>
    public interface IBootableDiskProviderService
    {
        /// <summary>
        /// Retrieves a list of available bootable disk models.
        /// </summary>
        /// <returns>List of bootable disks.</returns>
        public Task<List<BootableDiskModelDTO>> GetAvailableBootableDiskModels();
        
        /// <summary>
        /// Retrieves the path to a bootable disk by its identifier.
        /// </summary>
        /// <param name="id">Disk identifier.</param>
        /// <returns>Path to the disk or null if not found.</returns>
        public Task<string?> GetBootableDiskPathById(int id);
    }
}
