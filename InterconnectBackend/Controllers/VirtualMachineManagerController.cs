using Mappers;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(BaseResponse<object>.WithEmptyBaseResponse());
        }

        [HttpGet]
        public ActionResult<BaseResponse<ConnectionInfoResponse>> ConnectionInfo()
        {
            var connectionInfo = _vmManagerService.GetConnectionInfo();

            var connectionInfoResponse = ConnectionInfoToConnectionInfoResponse.Map(connectionInfo);

            return Ok(BaseResponse<ConnectionInfoResponse>.WithSuccess(connectionInfoResponse));
        }
    }
}
