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

        /// <summary>
        /// Initializes a new instance of the HypervisorConnectionController.
        /// </summary>
        /// <param name="hypervisorConnectionService">Service for managing hypervisor connection.</param>
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
