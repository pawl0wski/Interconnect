using Mappers;
using Models.Database;
using Models.DTO;
using Repositories;

namespace Services.Impl
{
    public class VirtualMachineEntityService : IVirtualMachineEntityService
    {
        private readonly IVirtualMachineEntityRepository _repository;
        private readonly IVirtualMachineManagerService _machineManagerService;

        public VirtualMachineEntityService(IVirtualMachineEntityRepository repository, IVirtualMachineManagerService machineManagerService)
        {
            _repository = repository;
            _machineManagerService = machineManagerService;
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

            List<VirtualMachineEntityDTO> entitiesDto = [.. entities.Select(VirtualMachineEntityMapper.MapToDTO)];
            var virtualMachines = _machineManagerService.GetListOfVirtualMachines();
            entitiesDto = [.. entitiesDto.Select(e =>
            {
                var vm = virtualMachines.FirstOrDefault(v => v.Uuid == e.VmUuid.ToString());
                if (vm is null)
                {
                    return e;
                }

                e.State = (Models.Enums.VirtualMachineState)vm.State;
                return e;
            })];

            return entitiesDto;
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
