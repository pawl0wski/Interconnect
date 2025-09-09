using Models.Database;

namespace Repositories
{
    public interface IInternetEntityRepository
    {
        public Task<InternetEntityModel> Create(string bridgeName, Guid uuid);
        public Task<List<InternetEntityModel>> GetAll();
        public Task<InternetEntityModel> GetById(int id);
        public Task<InternetEntityModel> UpdatePosition(int id, int x, int y);
    }
}
