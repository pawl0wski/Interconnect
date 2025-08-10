using Microsoft.AspNetCore.Mvc;
using Models;
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
        public ActionResult<VirtualMachineInfoResponse> CreateVirtualMachine(VirtualMachineCreateDefinition def)
        {
            var vmInfo = _vmManagerService.CreateVirtualMachine(def);

            return Ok(VirtualMachineInfoResponse.WithSuccess(vmInfo));
        }

        [HttpGet]
        public ActionResult<VirtualMachinesInfoResponse> GetListOfVirtualMachines()
        {
            var vms = _vmManagerService.GetListOfVirtualMachines();

            return Ok(VirtualMachinesInfoResponse.WithSuccess(vms));
        }
    }
}
