using Models.Database;

namespace Repositories
{
    public interface IVirtualMachineEntityRepository
    {
        public Task Add(VirtualMachineEntityModel model);
        public Task<List<VirtualMachineEntityModel>> GetAll();
        public Task<VirtualMachineEntityModel> GetById(int id);
        public Task Update(VirtualMachineEntityModel model);
    }
}
