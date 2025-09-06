using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualNetworkEntityConnectionRepository : IVirtualNetworkEntityConnectionRepository
    {
        private InterconnectDbContext _context;

        public VirtualNetworkEntityConnectionRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task CreateNew(string bridgeName, Guid firstEntityUuid, Guid secondEntityUuid)
        {
            var newModel = new VirtualNetworkEntityConnectionModel
            {
                BridgeName = bridgeName,
                FirstEntityUuid = firstEntityUuid,
                SecondEntityUuid = secondEntityUuid
            };

            _context.VirtualNetworkEntityConnectionModels.Add(newModel);

            await _context.SaveChangesAsync();
        }

        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll()
        {
            return _context.VirtualNetworkEntityConnectionModels.ToListAsync();
        }

        public async Task<VirtualNetworkEntityConnectionModel> GetForEntityUuid(Guid uuid)
        {
            var model = await _context.VirtualNetworkEntityConnectionModels
                .FirstAsync(m => m.FirstEntityUuid == uuid || m.SecondEntityUuid == uuid);

            return model;
        }
    }
}
