using Mappers;
using Models;
using Models.Database;
using Models.DTO;
using Models.Enums;
using Repositories;
using System.Xml.Linq;

namespace Services.Impl
{
    /// <summary>
    /// Service managing virtual machine entities.
    /// </summary>
    public class VirtualMachineEntityService : IVirtualMachineEntityService
    {
        private readonly IVirtualMachineEntityRepository _repository;
        private readonly IBootableDiskProviderService _bootableDiskProviderService;
        private readonly IVirtualMachineManagerService _vmManagerService;
        private readonly IVirtualMachineEntityNetworkInterfaceRepository _networkInterfaceRepository;

        /// <summary>
        /// Initializes a new instance of the VirtualMachineEntityService.
        /// </summary>
        /// <param name="repository">Repository for virtual machine entities.</param>
        /// <param name="bootableDiskProviderService">Service for managing bootable disks.</param>
        /// <param name="vmManagerService">Service for managing virtual machines.</param>
        /// <param name="networkInterfaceRepository">Repository for network interfaces.</param>
        public VirtualMachineEntityService(
            IVirtualMachineEntityRepository repository,
            IBootableDiskProviderService bootableDiskProviderService,
            IVirtualMachineManagerService vmManagerService,
            IVirtualMachineEntityNetworkInterfaceRepository networkInterfaceRepository)
        {
            _repository = repository;
            _bootableDiskProviderService = bootableDiskProviderService;
            _vmManagerService = vmManagerService;
            _networkInterfaceRepository = networkInterfaceRepository;
        }

        /// <summary>
        /// Creates a new virtual machine entity.
        /// </summary>
        /// <param name="name">Virtual machine name.</param>
        /// <param name="bootableDiskId">Bootable disk identifier.</param>
        /// <param name="memory">Amount of RAM in MB.</param>
        /// <param name="virtualCpus">Number of virtual CPUs.</param>
        /// <param name="type">Virtual machine entity type.</param>
        /// <param name="x">X coordinate position.</param>
        /// <param name="y">Y coordinate position.</param>
        /// <returns>Created virtual machine entity.</returns>
        public async Task<VirtualMachineEntityDTO> CreateEntity(string name, int bootableDiskId, uint memory, uint virtualCpus, VirtualMachineEntityType type, int x, int y)
        {
            var bootableDiskPath = await _bootableDiskProviderService.GetBootableDiskPathById(bootableDiskId);
            if (bootableDiskPath == null)
            {
                throw new Exception("Provided unknown bootableDiskId");
            }

            var vmCreateDefinition = new VirtualMachineCreateDefinition
            {
                Name = name,
                VirtualCpus = virtualCpus,
                Memory = memory,
                BootableDiskPath = bootableDiskPath
            };

            var vmInfo = _vmManagerService.CreateVirtualMachine(vmCreateDefinition);

            var entity = new VirtualMachineEntityModel
            {
                Name = name,
                Type = type,
                X = x,
                Y = y
            };

            await _repository.Add(entity);
            await UpdateEntityVmUUID(entity.Id, vmInfo.Uuid);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        /// <summary>
        /// Retrieves a list of all virtual machine entities.
        /// </summary>
        /// <returns>List of virtual machine entities.</returns>
        public async Task<List<VirtualMachineEntityDTO>> GetEntities()
        {
            var entities = await _repository.GetAll();

            List<VirtualMachineEntityDTO> entitiesDto = [.. entities.Select(VirtualMachineEntityMapper.MapToDTO)];
            var virtualMachines = _vmManagerService.GetListOfVirtualMachines();
            entitiesDto = [.. entitiesDto.Select(e =>
            {
                var vm = virtualMachines.FirstOrDefault(v => v.Uuid == e.VmUuid.ToString());
                if (vm is null)
                {
                    return e;
                }

                e.State = (VirtualMachineState)vm.State;
                return e;
            })];

            return entitiesDto;
        }

        /// <summary>
        /// Retrieves a list of virtual machine MAC addresses.
        /// </summary>
        /// <returns>List of MAC addresses.</returns>
        public async Task<List<VirtualMachineEntityMacAddressDTO>> GetMacAddresses()
        {
            var entities = await _repository.GetAll();
            var result = new List<VirtualMachineEntityMacAddressDTO>();

            foreach (var vm in entities)
            {
                var interfaces = await _networkInterfaceRepository.GetByVirtualMachineId(vm.Id);

                var macAddresses = interfaces
                    .Select(ni =>
                    {
                        var mac = XElement.Parse(ni.Definition)
                            .Elements("mac")
                            .Select(e => e.Attribute("address")?.Value)
                            .FirstOrDefault();
                        return mac;
                    })
                    .Where(mac => !string.IsNullOrWhiteSpace(mac))
                    .Distinct()
                    .ToList();

                result.Add(new VirtualMachineEntityMacAddressDTO
                {
                    VirtualMachineEntityId = vm.Id,
                    MacAddresses = macAddresses
                });
            }

            return result;
        }

        /// <summary>
        /// Retrieves a virtual machine entity by identifier.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <returns>Virtual machine entity.</returns>
        public async Task<VirtualMachineEntityDTO> GetById(int id)
        {
            var entity = await _repository.GetById(id);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        /// <summary>
        /// Updates the position of a virtual machine entity on the board.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="x">New X coordinate.</param>
        /// <param name="y">New Y coordinate.</param>
        /// <returns>Updated entity.</returns>
        public async Task<VirtualMachineEntityDTO> UpdateEntityPosition(int id, int x, int y)
        {
            var entity = await _repository.GetById(id);

            entity.X = x;
            entity.Y = y;

            await _repository.Update(entity);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }

        /// <summary>
        /// Updates the virtual machine UUID for an entity.
        /// </summary>
        /// <param name="id">Entity identifier.</param>
        /// <param name="uuid">New virtual machine UUID.</param>
        /// <returns>Updated entity.</returns>
        public async Task<VirtualMachineEntityDTO> UpdateEntityVmUUID(int id, string uuid)
        {
            var entity = await _repository.GetById(id);

            entity.VmUuid = Guid.Parse(uuid);
            await _repository.Update(entity);

            return VirtualMachineEntityMapper.MapToDTO(entity);
        }
    }
}
