using Library.Models;
using Mappers;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public ActionResult InitializeConnection()
        {
            _vmManagerService.InitializeConnection();

            return Ok();
        }

        [HttpGet]
        public ActionResult<ConnectionInfoResponse> ConnectionInfo()
        {
            var connectionInfo = _vmManagerService.GetConnectionInfo();

            return Ok(ConnectionInfoToConnectionInfoResponse.Map(connectionInfo));
        }
    }
}
