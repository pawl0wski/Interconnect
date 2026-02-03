using Microsoft.AspNetCore.Mvc;
using Services;
using Models.Responses;

namespace Controllers
{
    /// <summary>
    /// Controller providing system information.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class InfoController : ControllerBase
    {
        private IInfoService _infoService;

        /// <summary>
        /// Initializes a new instance of the InfoController.
        /// </summary>
        /// <param name="infoService">Service for providing system information.</param>
        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }

        /// <summary>
        /// Retrieves operating system information.
        /// </summary>
        /// <returns>System information.</returns>
        [HttpGet]
        public ActionResult<SystemInfoResponse> GetSystemInfo()
        {
            var info = _infoService.GetSystemInfo();

            return Ok(new SystemInfoResponse {
                Os = info.OsDescription,
                Arch = info.OsArch
            });
        }
    }
}
