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

        public VirtualMachineManagerController(IVirtualMachineManagerService vmManagerService)
        {
            _vmManagerService = vmManagerService;
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
    }
}
