using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualMachineEntityRepository : IVirtualMachineEntityRepository
    {
        private readonly InterconnectDbContext _context;
        
        public VirtualMachineEntityRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task Add(VirtualMachineEntityModel model)
        {
            _context.VirtualMachineEntityModels.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task<List<VirtualMachineEntityModel>> GetAll()
        {
            return await _context.VirtualMachineEntityModels.ToListAsync();
        }

        public async Task<VirtualMachineEntityModel> GetById(int id)
        {
            return await _context.VirtualMachineEntityModels.Where((e) => e.Id == id).SingleAsync();
        }

        public async Task Update(VirtualMachineEntityModel model)
        {
            _context.VirtualMachineEntityModels.Update(model);
            await _context.SaveChangesAsync();
        }
    }
}
