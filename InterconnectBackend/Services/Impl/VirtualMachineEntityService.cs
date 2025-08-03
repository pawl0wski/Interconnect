using Database;
using Mappers;
using Microsoft.EntityFrameworkCore;
using Models.Database;
using Models.DTO;

namespace Services.Impl
{
    public class VirtualMachineEntityService : IVirtualMachineEntityService
    {
        private readonly InterconnectDbContext _context;

        public VirtualMachineEntityService(InterconnectDbContext context)
        {
            _context = context;
        }

        public async Task<VirtualMachineEntityDTO> CreateEntity(string name, int x, int y)
        {
            var entity = new VirtualMachineEntityModel
            {
                Name = name,
                X = x,
                Y = y
            };

            _context.VirtualMachineEntityModels.Add(entity);
            await _context.SaveChangesAsync();

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public Task<List<VirtualMachineEntityDTO>> GetEntities()
        {
            return _context.VirtualMachineEntityModels
                .Select((m) => VirtualMachineEntityMapper.MapToDTO(m))
                .ToListAsync();
        }

        public async Task<VirtualMachineEntityDTO> GetEntityById(int id)
        {
            var entity = await _context.VirtualMachineEntityModels.Where((e) => e.Id == id).SingleAsync();

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public async Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y)
        {
            var entity = await _context.VirtualMachineEntityModels
                .Where((e) => e.Id == id)
                .SingleAsync();

            entity.X = x;
            entity.Y = y;

            _context.VirtualMachineEntityModels.Update(entity);
            await _context.SaveChangesAsync();

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public async Task<VirtualMachineEntityDTO> UpdateEntityVmUUID(int id, string uuid)
        {
            var entity = await _context.VirtualMachineEntityModels
                   .Where((e) => e.Id == id)
                   .SingleAsync();

            entity.VmUuid = Guid.Parse(uuid);
            _context.VirtualMachineEntityModels.Update(entity);
            await _context.SaveChangesAsync();

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }
    }
}
