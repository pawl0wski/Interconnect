using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class BootableDiskRepository : IBootableDiskRepository
    {
        private InterconnectDbContext _context { get; set; }

        public BootableDiskRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<List<BootableDiskModel>> GetOnlyWithNotNullablePath()
        {
            return await _context.BootableDiskModels.Where(m => m.Path != null).ToListAsync();
        }

        public async Task<BootableDiskModel> GetById(int id)
        {
            return await _context.BootableDiskModels.FirstAsync(m => m.Id == id);
        }
    }
}
