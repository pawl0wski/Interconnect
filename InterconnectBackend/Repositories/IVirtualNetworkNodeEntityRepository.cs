using Models.Database;

namespace Repositories
{
    public interface IVirtualNetworkNodeEntityRepository
    {
        public Task<VirtualNetworkNodeEntityModel> Create(string name, VirtualNetworkModel virtualNetwork);
        public Task<VirtualNetworkNodeEntityModel> CreateInvisible(VirtualNetworkModel virtualNetwork);
        public Task<List<VirtualNetworkNodeEntityModel>> GetAll();
        public Task<List<VirtualNetworkNodeEntityModel>> GetVisible();
        public Task<VirtualNetworkNodeEntityModel> GetById(int id);
        public Task<VirtualNetworkNodeEntityModel> UpdateEntityPosition(int id, int x, int y);
        public Task<VirtualNetworkNodeEntityModel> UpdateNetwork(int id, VirtualNetworkModel virtualNetwork);
        public Task Remove(int id);
    }
}
