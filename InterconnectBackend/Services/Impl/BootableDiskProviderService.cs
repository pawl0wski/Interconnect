using Repositories;
using Mappers;
using Models.DTO;

namespace Services.Impl
{
    /// <summary>
    /// Service providing information about available bootable disks.
    /// </summary>
    public class BootableDiskProviderService : IBootableDiskProviderService
    {
        private readonly IBootableDiskRepository _repository;

        /// <summary>
        /// Initializes a new instance of the BootableDiskProviderService.
        /// </summary>
        /// <param name="repository">Repository for bootable disk models.</param>
        public BootableDiskProviderService(IBootableDiskRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Retrieves a list of available bootable disk models.
        /// </summary>
        /// <returns>List of bootable disks.</returns>
        public async Task<List<BootableDiskModelDTO>> GetAvailableBootableDiskModels()
        {
            var bootableDisks = await _repository.GetOnlyWithNotNullablePath();

            bootableDisks = [.. bootableDisks.Where(m => File.Exists(m.Path))];

            return [.. bootableDisks.Select(BootableDiskModelMapper.MapToDTO)];
        }

        /// <summary>
        /// Retrieves the path to a bootable disk by its identifier.
        /// </summary>
        /// <param name="id">Disk identifier.</param>
        /// <returns>Path to the disk or null if not found.</returns>
        public async Task<string?> GetBootableDiskPathById(int id)
        {
            var bootableDisk = await _repository.GetById(id);

            return bootableDisk.Path;
        }
    }
}
