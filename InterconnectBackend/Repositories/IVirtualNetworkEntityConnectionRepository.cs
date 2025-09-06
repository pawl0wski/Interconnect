using Models.Database;

namespace Repositories
{
    public interface IVirtualNetworkEntityConnectionRepository
    {
        public Task<VirtualNetworkEntityConnectionModel> GetForEntityUuid(Guid uuid);
        public Task Create(Guid firstEntityUuid, Guid secondEntityUuid);
        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll();
    }
}
