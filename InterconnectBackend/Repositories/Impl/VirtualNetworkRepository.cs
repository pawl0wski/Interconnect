using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualNetworkRepository : IVirtualNetworkRepository
    {
        private InterconnectDbContext _context;

        public VirtualNetworkRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid)
        {
            var virtualNetwork = new VirtualNetworkModel
            {
                BridgeName = bridgeName,
                Uuid = uuid
            };

            _context.VirtualNetworkModels.Add(virtualNetwork);
            await _context.SaveChangesAsync();

            return virtualNetwork;
        }

        public async Task<VirtualNetworkModel> GetById(int id)
        {
            return await _context.VirtualNetworkModels.FirstAsync(x => x.Id == id);
        }
    }
}
