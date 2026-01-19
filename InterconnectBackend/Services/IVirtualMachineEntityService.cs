using Models.DTO;
using Models.Enums;

namespace Services
{
    public interface IVirtualMachineEntityService
    {
        public Task<List<VirtualMachineEntityDTO>> GetEntities();
        public Task<List<VirtualMachineEntityMacAddressDTO>> GetMacAddresses();
        public Task<VirtualMachineEntityDTO> CreateEntity(string name, int bootableDiskId, uint memory, uint virtualCpus, VirtualMachineEntityType type, int x, int y);
        public Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y);
        public Task<VirtualMachineEntityDTO> GetById(int id);
        public Task<VirtualMachineEntityDTO> UpdateEntityVmUUID(int id, string uuid);
    }
}
