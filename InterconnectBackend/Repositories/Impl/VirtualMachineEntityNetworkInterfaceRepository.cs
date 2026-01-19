using Database;
using Microsoft.EntityFrameworkCore;
using Models.Database;

namespace Repositories.Impl
{
    public class VirtualMachineEntityNetworkInterfaceRepository : IVirtualMachineEntityNetworkInterfaceRepository
    {
        private readonly InterconnectDbContext _context;

        public VirtualMachineEntityNetworkInterfaceRepository(InterconnectDbContext context)
        {
            _context = context;
        }

        public Task Create(VirtualMachineEntityNetworkInterfaceModel networkInterface)
        {
            _context.VirtualMachineEntityNetworkInterfaceModels.Add(networkInterface);
            return _context.SaveChangesAsync();
        }

        public Task Update(VirtualMachineEntityNetworkInterfaceModel networkInterface)
        {
            _context.VirtualMachineEntityNetworkInterfaceModels.Update(networkInterface);
            return _context.SaveChangesAsync();
        }

        public Task Remove(VirtualMachineEntityNetworkInterfaceModel networkInterface)
        {
            _context.VirtualMachineEntityNetworkInterfaceModels.Remove(networkInterface);
            return _context.SaveChangesAsync();
        }

        public Task<VirtualMachineEntityNetworkInterfaceModel?> GetByIds(int virtualMachineId, int connectionId)
            => _context.VirtualMachineEntityNetworkInterfaceModels.Where(
                x => x.VirtualMachineEntityId == virtualMachineId
                && x.VirtualNetworkEntityConnectionId == connectionId)
            .FirstOrDefaultAsync();

        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByNetworkName(int virtualMachineId, string networkName)
            => _context.VirtualMachineEntityNetworkInterfaceModels
            .Where(x => x.VirtualMachineEntityId == virtualMachineId
                && EF.Functions.Like(x.Definition, $"%{networkName}%"))
            .ToListAsync();

        public Task<List<VirtualMachineEntityNetworkInterfaceModel>> GetByVirtualMachineId(int id) 
            => _context.VirtualMachineEntityNetworkInterfaceModels
                .Where(x => x.VirtualMachineEntityId == id)
                .ToListAsync();
    }
}
