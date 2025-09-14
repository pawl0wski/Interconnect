using Models.Database;
using Models.Enums;

namespace Repositories
{
    public interface IVirtualNetworkConnectionRepository
    {
        public Task<List<VirtualNetworkEntityConnectionModel>> GetUsingEntityId(int id, EntityType type);
        public Task<VirtualNetworkEntityConnectionModel> Create(int firstEntityId, EntityType firstEntityType, int secondEntityId, EntityType secondEntityType);
        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll();
    }
}
