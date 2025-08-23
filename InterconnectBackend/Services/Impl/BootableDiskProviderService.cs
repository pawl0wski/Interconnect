using Repositories;
using Mappers;
using Models.DTO;

namespace Services.Impl
{
    public class BootableDiskProviderService : IBootableDiskProviderService
    {
        private readonly IBootableDiskRepository _repository;

        public BootableDiskProviderService(IBootableDiskRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<BootableDiskModelDTO>> GetAvailableBootableDiskModels()
        {
            var bootableDisks = await _repository.GetOnlyWithNotNullablePath();

            bootableDisks = [.. bootableDisks.Where(m => File.Exists(m.Path))];

            return [.. bootableDisks.Select(BootableDiskModelMapper.MapToDTO)];
        }

        public async Task<string?> GetBootableDiskPathById(int id)
        {
            var bootableDisk = await _repository.GetById(id);

            return bootableDisk.Path;
        }
    }
}
