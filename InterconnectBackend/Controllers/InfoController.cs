using Microsoft.AspNetCore.Mvc;
using Services;
using Models.Responses;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class InfoController : ControllerBase
    {
        private IInfoService _infoService;

        public InfoController(IInfoService infoService)
        {
            _infoService = infoService;
        }

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
