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

        public async Task<VirtualSwitchEntityModel> Create(string name, VirtualNetworkModel virtualNetwork)
        {
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                Name = name,
                Visible = true,
                VirtualNetwork = virtualNetwork
            };
            await _context.VirtualSwitchEntityModels.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            return virtualSwitch;
        }

        public async Task<VirtualSwitchEntityModel> CreateInvisible(VirtualNetworkModel virtualNetwork)
        {
            var virtualSwitch = new VirtualSwitchEntityModel
            {
                VirtualNetwork = virtualNetwork,
                Visible = false
            };
            await _context.VirtualSwitchEntityModels.AddAsync(virtualSwitch);
            await _context.SaveChangesAsync();

            return virtualSwitch;
        }

        public Task<List<VirtualSwitchEntityModel>> GetAll()
        {
            return _context.VirtualSwitchEntityModels.Include(x => x.VirtualNetwork).ToListAsync();
        }

        public Task<VirtualSwitchEntityModel> GetById(int id)
        {
            return _context.VirtualSwitchEntityModels.Include(x => x.VirtualNetwork).FirstAsync(m => m.Id == id);
        }

        public Task<List<VirtualSwitchEntityModel>> GetVisible()
        {
            return _context.VirtualSwitchEntityModels.Include(x => x.VirtualNetwork).Where(m => m.Visible).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var virtualSwitch = await GetById(id);

            _context.VirtualSwitchEntityModels.Remove(virtualSwitch);
            await _context.SaveChangesAsync();
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

        public async Task<VirtualSwitchEntityModel> UpdateNetwork(int id, VirtualNetworkModel virtualNetwork)
        {
            var model = await GetById(id);
            model.VirtualNetwork = virtualNetwork;

            _context.VirtualSwitchEntityModels.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
