using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualNetworkNodeEntityRepository : IVirtualNetworkNodeEntityRepository
    {
        private readonly InterconnectDbContext _context;

        public VirtualNetworkNodeEntityRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<VirtualNetworkNodeEntityModel> Create(string name, VirtualNetworkModel virtualNetwork)
        {
            var virtualNetworkNode = new VirtualNetworkNodeEntityModel
            {
                Name = name,
                Visible = true,
                VirtualNetwork = virtualNetwork
            };
            await _context.VirtualNetworkNodeEntityModels.AddAsync(virtualNetworkNode);
            await _context.SaveChangesAsync();

            return virtualNetworkNode;
        }

        public async Task<VirtualNetworkNodeEntityModel> CreateInvisible(VirtualNetworkModel virtualNetwork)
        {
            var virtualNetworkNode = new VirtualNetworkNodeEntityModel
            {
                VirtualNetwork = virtualNetwork,
                Visible = false
            };
            await _context.VirtualNetworkNodeEntityModels.AddAsync(virtualNetworkNode);
            await _context.SaveChangesAsync();

            return virtualNetworkNode;
        }

        public Task<List<VirtualNetworkNodeEntityModel>> GetAll()
        {
            return _context.VirtualNetworkNodeEntityModels.Include(x => x.VirtualNetwork).ToListAsync();
        }

        public Task<VirtualNetworkNodeEntityModel> GetById(int id)
        {
            return _context.VirtualNetworkNodeEntityModels.Include(x => x.VirtualNetwork).FirstAsync(m => m.Id == id);
        }

        public Task<List<VirtualNetworkNodeEntityModel>> GetVisible()
        {
            return _context.VirtualNetworkNodeEntityModels.Include(x => x.VirtualNetwork).Where(m => m.Visible).ToListAsync();
        }

        public async Task Remove(int id)
        {
            var virtualNetworkNode = await GetById(id);

            _context.VirtualNetworkNodeEntityModels.Remove(virtualNetworkNode);
            await _context.SaveChangesAsync();
        }

        public async Task<VirtualNetworkNodeEntityModel> UpdateEntityPosition(int id, int x, int y)
        {
            var model = await GetById(id);
            model.X = x;
            model.Y = y;

            _context.VirtualNetworkNodeEntityModels.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<VirtualNetworkNodeEntityModel> UpdateNetwork(int id, VirtualNetworkModel virtualNetwork)
        {
            var model = await GetById(id);
            model.VirtualNetwork = virtualNetwork;

            _context.VirtualNetworkNodeEntityModels.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
