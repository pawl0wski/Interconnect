using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Models.Enums;

namespace Repositories.Impl
{
    public class VirtualNetworkConnectionRepository : IVirtualNetworkConnectionRepository
    {
        private InterconnectDbContext _context;

        public VirtualNetworkConnectionRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<VirtualNetworkEntityConnectionModel> Create(int firstEntityId, EntityType firstEntityType, int secondEntityId, EntityType secondEntityType)
        {
            var newModel = new VirtualNetworkEntityConnectionModel
            {
                SourceEntityId = firstEntityId,
                SourceEntityType = firstEntityType,
                DestinationEntityId = secondEntityId,
                DestinationEntityType = secondEntityType
            };

            _context.VirtualNetworkEntityConnectionModels.Add(newModel);

            await _context.SaveChangesAsync();

            return newModel;
        }

        public Task<List<VirtualNetworkEntityConnectionModel>> GetAll()
        {
            return _context.VirtualNetworkEntityConnectionModels.ToListAsync();
        }

        public async Task<VirtualNetworkEntityConnectionModel> GetUsingEntityId(int id)
        {
            var model = await _context.VirtualNetworkEntityConnectionModels
                .FirstAsync(m => m.SourceEntityId == id || m.DestinationEntityId == id);

            return model;
        }
    }
}
