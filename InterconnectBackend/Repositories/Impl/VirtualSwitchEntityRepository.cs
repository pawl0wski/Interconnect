using Database;
using Microsoft.EntityFrameworkCore;
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

        public async Task<VirtualSwitchEntityModel> Create(string name, string bridge, Guid uuid)
        {
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                Name = name,
                BridgeName = bridge,
                Uuid = uuid,
                Visible = true,
            };
            await _context.VirtualSwitchEntityModels.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            return virtualSwitch;
        }

        public async Task<VirtualSwitchEntityModel> CreateInvisible(string bridge, Guid uuid)
        {
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                BridgeName = bridge,
                Uuid = uuid,
                Visible = false
            };
            await _context.VirtualSwitchEntityModels.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            return virtualSwitch;
        }

        public Task<List<VirtualSwitchEntityModel>> GetAll()
        {
            return _context.VirtualSwitchEntityModels.ToListAsync();
        }

        public Task<VirtualSwitchEntityModel> GetById(int id)
        {
            return _context.VirtualSwitchEntityModels.FirstAsync(m => m.Id == id);
        }

        public Task<List<VirtualSwitchEntityModel>> GetVisible()
        {
            return _context.VirtualSwitchEntityModels.Where(m => m.Visible).ToListAsync();
        }

        public async Task<VirtualSwitchEntityModel> UpdateEntityPosition(int id, int x, int y)
        {
            var model = await GetById(id);
            model.X = x;
            model.Y = y;

            _context.VirtualSwitchEntityModels.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
