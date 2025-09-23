using Models.Database;

namespace Repositories
{
    public interface IVirtualNetworkRepository
    {
        public Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid);
        public Task<VirtualNetworkModel> GetById(int id);
        public Task<VirtualNetworkModel> GetByUuid(Guid uuid);
        public Task<VirtualNetworkModel> GetByUuidWithVirtualSwitches(Guid uuid);
        public Task Remove(int id);
    }
}
