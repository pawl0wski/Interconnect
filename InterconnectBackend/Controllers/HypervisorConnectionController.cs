using Microsoft.AspNetCore.Mvc;
using Models;
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

        [HttpGet]
        public ActionResult<BaseResponse<string>> Ping()
        {
            return Ok(BaseResponse<string>.WithSuccess("pong"));
        }

        [HttpGet]
        public ActionResult<BaseResponse<ConnectionInfo>> ConnectionInfo()
        {
            var connectionInfo = _hypervisorConnectionService.GetConnectionInfo();

            return Ok(BaseResponse<ConnectionInfo>.WithSuccess(connectionInfo));
        }
    }
}
