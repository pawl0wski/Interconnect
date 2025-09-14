using Models.Database;

namespace Repositories
{
    public interface IVirtualSwitchEntityRepository
    {
        public Task<VirtualSwitchEntityModel> Create(string name, VirtualNetworkModel virtualNetwork);
        public Task<VirtualSwitchEntityModel> CreateInvisible(VirtualNetworkModel virtualNetwork);
        public Task<List<VirtualSwitchEntityModel>> GetAll();
        public Task<List<VirtualSwitchEntityModel>> GetVisible();
        public Task<VirtualSwitchEntityModel> GetById(int id);
        public Task<VirtualSwitchEntityModel> UpdateEntityPosition(int id, int x, int y);
        public Task<VirtualSwitchEntityModel> UpdateNetwork(int id, VirtualNetworkModel virtualNetwork);
    }
}
