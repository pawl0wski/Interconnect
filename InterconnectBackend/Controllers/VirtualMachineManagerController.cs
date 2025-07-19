using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualMachineManagerController : ControllerBase
    {
        private IVirtualMachineManagerService _vmManagerService;

        public VirtualMachineManagerController(IVirtualMachineManagerService vmManagerService)
        {
            _vmManagerService = vmManagerService;
        }

        [HttpPost]
        public ActionResult<BaseResponse<object>> InitializeConnection(InitializeConnectionRequest req)
        {
            _vmManagerService.InitializeConnection(req.ConnectionUrl);

            return Ok(BaseResponse<object>.WithEmptySuccess());
        }

        [HttpGet]
        public ActionResult<BaseResponse<ConnectionInfo>> ConnectionInfo()
        {
            var connectionInfo = _vmManagerService.GetConnectionInfo();

            return Ok(BaseResponse<ConnectionInfo>.WithSuccess(connectionInfo));
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
