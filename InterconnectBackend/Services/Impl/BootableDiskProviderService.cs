using Database;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Models.DTO;

namespace Services.Impl
{
    public class BootableDiskProviderService : IBootableDiskProviderService
    {
        private InterconnectDbContext _context { get; set; }

        public BootableDiskProviderService(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<List<BootableDiskModelDTO>> GetAvailableBootableDiskModels()
        {
            var bootableDisks = await _context.BootableDiskModels.Where(m => m.Path != null).ToListAsync();

            bootableDisks = [.. bootableDisks.Where(m => File.Exists(m.Path))];

            return [.. bootableDisks.Select(BootableDiskModelMapper.MapToDTO)];
        }

        public async Task<string?> GetBootableDiskPathById(int id)
        {
            var bootableDisk = await _context.BootableDiskModels.FirstAsync(m => m.Id == id);

            return bootableDisk.Path;
        }
    }
}
