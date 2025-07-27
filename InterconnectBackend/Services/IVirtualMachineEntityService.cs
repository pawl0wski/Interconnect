using Models.DTO;

namespace Services
{
    public interface IVirtualMachineEntityService
    {
        public Task<List<VirtualMachineEntityDTO>> GetEntities();
        public Task<VirtualMachineEntityDTO> CreateEntity(string name, int x, int y);
        public Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y);
        public Task<VirtualMachineEntityDTO> GetEntityById(int id);
    }
}
