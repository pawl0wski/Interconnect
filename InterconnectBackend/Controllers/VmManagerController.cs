using Library.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VmManagerController : ControllerBase
    {
        private IVirtualMachineManagerWrapper _vmManagerService;

        public VmManagerController(IVirtualMachineManagerWrapper vmManagerService)
        {
            _vmManagerService = vmManagerService;
        }

        [HttpGet]
        public ActionResult InitializeConnection()
        {
            _vmManagerService.InitializeConnection(null);

            return Ok();
        }
    }
}
