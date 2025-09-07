using Models.Database;
using Models.Enums;

namespace Repositories
{
    public interface IVirtualNetworkConnectionRepository
    {
        public Task<VirtualNetworkEntityConnectionModel> GetUsingEntityId(int id);
        public Task<VirtualNetworkEntityConnectionModel> Create(int firstEntityId, EntityType firstEntityType, int secondEntityId, EntityType secondEntityType);
        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll();
    }
}
