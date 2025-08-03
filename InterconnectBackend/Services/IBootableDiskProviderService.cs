using Models.DTO;

namespace Services
{
    public interface IBootableDiskProviderService
    {
        public Task<List<BootableDiskModelDTO>> GetAvailableBootableDiskModels();
        public Task<string?> GetBootableDiskPathById(int id);
    }
}
