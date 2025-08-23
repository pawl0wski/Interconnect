using Models.Database;

namespace Repositories
{
    public interface IBootableDiskRepository
    {
        public Task<List<BootableDiskModel>> GetOnlyWithNotNullablePath();
        public Task<BootableDiskModel> GetById(int id);
    }
}
