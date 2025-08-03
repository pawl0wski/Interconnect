using Microsoft.AspNetCore.Mvc;
using Models;
using Models.DTO;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualMachineManagerController : ControllerBase
    {
        private readonly IVirtualMachineManagerService _vmManagerService;
        private readonly IBootableDiskProviderService _bootableDiskProviderService;

        public VirtualMachineManagerController(IVirtualMachineManagerService vmManagerService, IBootableDiskProviderService bootableDiskProviderService)
        {
            _vmManagerService = vmManagerService;
            _bootableDiskProviderService = bootableDiskProviderService;
        }

        [HttpPost]
        public ActionResult<BaseResponse<VirtualMachineInfo>> CreateVirtualMachine(VirtualMachineCreateDefinition def)
        {
            var vmInfo = _vmManagerService.CreateVirtualMachine(def);

            return Ok(BaseResponse<VirtualMachineInfo>.WithSuccess(vmInfo));
        }

        [HttpGet]
        public ActionResult<BaseResponse<List<VirtualMachineInfo>>> GetListOfVirtualMachines()
        {
            var vms = _vmManagerService.GetListOfVirtualMachines();

            return Ok(BaseResponse<List<VirtualMachineInfo>>.WithSuccess(vms));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<BootableDiskModelDTO>>>> GetAvailableBootableDisks()
        {
            var bootableDisks = await _bootableDiskProviderService.GetAvailableBootableDiskModels();

            return Ok(BaseResponse<List<BootableDiskModelDTO>>.WithSuccess(bootableDisks));
        }
    }
}
