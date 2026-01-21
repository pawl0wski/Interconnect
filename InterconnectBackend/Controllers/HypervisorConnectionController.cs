using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller managing hypervisor connection.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class HypervisorConnectionController : ControllerBase
    {
        private IHypervisorConnectionService _hypervisorConnectionService;

        public HypervisorConnectionController(IHypervisorConnectionService hypervisorConnectionService)
        {
            _hypervisorConnectionService = hypervisorConnectionService;
        }

        /// <summary>
        /// Retrieves hypervisor connection information.
        /// </summary>
        /// <returns>Connection information.</returns>
        [HttpGet]
        public ActionResult<ConnectionInfoResponse> ConnectionInfo()
        {
            var connectionInfo = _hypervisorConnectionService.GetConnectionInfo();

            return Ok(ConnectionInfoResponse.WithSuccess(connectionInfo));
        }
    }
}
