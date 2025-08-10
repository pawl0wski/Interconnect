using Microsoft.AspNetCore.Mvc;
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
        public ActionResult<ConnectionInfoResponse> ConnectionInfo()
        {
            var connectionInfo = _hypervisorConnectionService.GetConnectionInfo();

            return Ok(ConnectionInfoResponse.WithSuccess(connectionInfo));
        }
    }
}
