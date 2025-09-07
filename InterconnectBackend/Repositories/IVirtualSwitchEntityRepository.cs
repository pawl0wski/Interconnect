using Models.Database;

namespace Repositories
{
    public interface IVirtualSwitchEntityRepository
    {
        public Task<VirtualSwitchEntityModel> Create(string name, string bridge, Guid uuid);
        public Task<VirtualSwitchEntityModel> CreateInvisible(string bridge, Guid uuid);
        public Task<List<VirtualSwitchEntityModel>> GetAll();
        public Task<List<VirtualSwitchEntityModel>> GetVisible();
        public Task<VirtualSwitchEntityModel> GetById(int id);
        public Task<VirtualSwitchEntityModel> UpdateEntityPosition(int id, int x, int y);
    }
}
