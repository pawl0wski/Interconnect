using Models.Database;

namespace Repositories
{
    public interface IVirtualNetworkRepository
    {
        public Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid);
        public Task<VirtualNetworkModel> GetById(int id);
    }
}
