using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class InfoController : ControllerBase
    {
        private readonly IVersionService _versionService;
        private readonly ILogger<InfoController> _logger;

        public InfoController(ILogger<InfoController> logger, IVersionService versionService)
        {
            _logger = logger;
            _versionService = versionService;
        }

        [HttpGet]
        public ActionResult<String> GetVersion()
        {
            return Ok(_versionService.GetVersion());
        }
    }
}
