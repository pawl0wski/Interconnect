using Models.Database;

namespace Repositories
{
    public interface IVirtualMachineEntityNetworkInterfaceRepository
    {
        public Task Create(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        public Task Update(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        public Task Remove(VirtualMachineEntityNetworkInterfaceModel networkInterface);
        public Task<VirtualMachineEntityNetworkInterfaceModel?> GetByIds(int virtualMachineId, int connectionId);
        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByVirtualMachineId(int id);
        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByNetworkName(int virtualMachineId, string networkName);
    }
}
