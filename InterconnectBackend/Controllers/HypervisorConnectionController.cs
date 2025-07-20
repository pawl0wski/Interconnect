using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Enums;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class HypervisorConnectionController : ControllerBase
    {
        private IHypervisorConnectionService _hypervisorConnectionService;

        public HypervisorConnectionController(IHypervisorConnectionService hypervisorConnectionService)
        {
            _hypervisorConnectionService = hypervisorConnectionService;
        }

        [HttpPost]
        public ActionResult<BaseResponse<object>> InitializeConnection(InitializeConnectionRequest req)
        {
            _hypervisorConnectionService.InitializeConnection(req.ConnectionUrl);

            return Ok(BaseResponse<object>.WithEmptySuccess());
        }

        [HttpPost]
        public ActionResult<BaseResponse<ConnectionStatus>> ConnectionStatus()
        {
            var connectionStatus = _hypervisorConnectionService.GetConnectionStatus();

            return Ok(BaseResponse<object>.WithSuccess(connectionStatus));
        }

        [HttpGet]
        public ActionResult<BaseResponse<ConnectionInfo>> ConnectionInfo()
        {
            var connectionInfo = _hypervisorConnectionService.GetConnectionInfo();

            return Ok(BaseResponse<ConnectionInfo>.WithSuccess(connectionInfo));
        }
    }
}
