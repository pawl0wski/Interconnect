using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class InternetEntityRepository : IInternetEntityRepository
    {
        private readonly InterconnectDbContext _context;

        public InternetEntityRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<InternetEntityModel> Create(string bridgeName, Guid uuid)
        {
            var model = new InternetEntityModel
            {
                BridgeName = bridgeName,
                Uuid = uuid,
                X = 0,
                Y = 0,
            };
            _context.InternetEntityModels.Add(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<List<InternetEntityModel>> GetAll()
        {
            return await _context.InternetEntityModels.ToListAsync();
        }

        public async Task<InternetEntityModel> GetById(int id)
        {
            return await _context.InternetEntityModels.FirstAsync(m => m.Id == id);
        }

        public async Task<InternetEntityModel> UpdatePosition(int id, int x, int y)
        {
            var model = await GetById(id);
            model.X = x;
            model.Y = y;

            _context.InternetEntityModels.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }
    }
}
