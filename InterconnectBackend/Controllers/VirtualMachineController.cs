using Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualMachineController : ControllerBase
    {
        private readonly IVirtualMachineManagerService _vmManagerService;
        private readonly IVirtualMachineEntityService _entityService;
        private readonly IBootableDiskProviderService _bootableDiskProviderService;

        public VirtualMachineController(IVirtualMachineManagerService vmManagerService, IVirtualMachineEntityService vmEntityService, IBootableDiskProviderService bootableDiskProviderService)
        {
            _vmManagerService = vmManagerService;
            _entityService = vmEntityService;
            _bootableDiskProviderService = bootableDiskProviderService;
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<VirtualMachineEntityDTO>>> CreateVirtualMachine(CreateVirtualMachineRequest req)
        {
            var bootableDiskPath = await _bootableDiskProviderService.GetBootableDiskPathById(req.BootableDiskId);
            if (bootableDiskPath == null)
            {
                throw new Exception("Provided unknown bootableDiskId");
            }

            var vmCreateDefinition = new VirtualMachineCreateDefinition
            {
                Name = req.Name,
                VirtualCpus = req.VirtualCPUs,
                Memory = req.Memory,
                BootableDiskPath = bootableDiskPath
            };

            var vmInfo = _vmManagerService.CreateVirtualMachine(vmCreateDefinition);
            var entity = await _entityService.CreateEntity(req.Name, 25, 25);
            var updatedEntity = await _entityService.UpdateEntityVmUUID(entity.Id, vmInfo.Uuid);

            return Ok(BaseResponse<VirtualMachineEntityDTO>.WithSuccess(updatedEntity));
        }
    }
}
