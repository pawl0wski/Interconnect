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

        public async Task<VirtualNetworkModel> Create(string bridgeName, Guid uuid, string? ipAddress = null)
        {
            var virtualNetwork = new VirtualNetworkModel
            {
                BridgeName = bridgeName,
                Uuid = uuid,
                IpAddress = ipAddress
            };

            _context.VirtualNetworkModels.Add(virtualNetwork);
            await _context.SaveChangesAsync();

            return virtualNetwork;
        }

        public Task<List<VirtualNetworkModel>> GetAll()
        {
            return _context.VirtualNetworkModels.ToListAsync();
        }

        public Task<VirtualNetworkModel> GetById(int id)
        {
            return _context.VirtualNetworkModels.FirstAsync(x => x.Id == id);
        }

        public Task<VirtualNetworkModel> GetByUuid(Guid uuid)
        {
            return _context.VirtualNetworkModels.FirstAsync(x => x.Uuid == uuid);
        }

        public Task<VirtualNetworkModel> GetByUuidWithVirtualSwitches(Guid uuid)
        {
            return _context.VirtualNetworkModels.Include(x => x.VirtualSwitches).FirstAsync(x => x.Uuid == uuid);
        }

        public async Task Remove(int id)
        {
            var virtualNetwork = await GetById(id);

            if (virtualNetwork is null)
            {
                throw new NullReferenceException("Can't find virtual network");
            }

            _context.VirtualNetworkModels.Remove(virtualNetwork);
            await _context.SaveChangesAsync();
        }
    }
}
