using Mappers;
using Models.Database;
using Models.DTO;
using Repositories;

namespace Services.Impl
{
    public class VirtualMachineEntityService : IVirtualMachineEntityService
    {
        private readonly IVirtualMachineEntityRepository _repository;

        public VirtualMachineEntityService(IVirtualMachineEntityRepository repository)
        {
            _repository = repository;
        }

        public async Task<VirtualMachineEntityDTO> CreateEntity(string name, int x, int y)
        {
            var entity = new VirtualMachineEntityModel
            {
                Name = name,
                X = x,
                Y = y
            };

            await _repository.Add(entity);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public async Task<List<VirtualMachineEntityDTO>> GetEntities()
        {
            var entities = await _repository.GetAll();

            return [.. entities.Select(VirtualMachineEntityMapper.MapToDTO)];
        }

        public async Task<VirtualMachineEntityDTO> GetEntityById(int id)
        {
            var entity = await _repository.GetById(id);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public async Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y)
        {
            var entity = await _repository.GetById(id);

            entity.X = x;
            entity.Y = y;

            await _repository.Update(entity);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        public async Task<VirtualMachineEntityDTO> UpdateEntityVmUUID(int id, string uuid)
        {
            var entity = await _repository.GetById(id);

            entity.VmUuid = Guid.Parse(uuid);
            await _repository.Update(entity);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }
    }
}
