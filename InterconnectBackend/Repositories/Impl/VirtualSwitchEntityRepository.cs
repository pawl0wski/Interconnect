using Database;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualSwitchEntityRepository : IVirtualSwitchEntityRepository
    {
        private readonly InterconnectDbContext _context;

        public VirtualSwitchEntityRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task Create(string name, string bridge, Guid uuid)
        {
            await _context.VirtualSwitchEntityModels.AddAsync(new VirtualSwitchEntityModel
            {
                Name = name,
                BridgeName = bridge,
                Uuid = uuid,
                Visible = true,
            });

            await _context.SaveChangesAsync();
        }

        public async Task CreateInvisible(string bridge, Guid uuid)
        {
            await _context.VirtualSwitchEntityModels.AddAsync(new VirtualSwitchEntityModel
            {
                BridgeName = bridge,
                Uuid = uuid,
                Visible = false
            });

            await _context.SaveChangesAsync();
        }
    }
}
