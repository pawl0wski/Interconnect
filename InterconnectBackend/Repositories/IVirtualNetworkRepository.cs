using Models.Database;

namespace Repositories
{
    public interface IVirtualNetworkRepository
    {
        public Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid, string? ipAddress = null);
        public Task<VirtualNetworkModel> GetById(int id);
        public Task<VirtualNetworkModel> GetByUuid(Guid uuid);
        public Task<VirtualNetworkModel> GetByUuidWithVirtualNetworkNodes(Guid uuid);
        public Task Remove(int id);
        public Task<List<VirtualNetworkModel>> GetAll();
        public Task<VirtualNetworkModel> GetByBridgeName(string bridgeName);
    }
}
