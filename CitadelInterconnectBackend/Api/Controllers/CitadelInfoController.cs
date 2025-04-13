using Microsoft.AspNetCore.Mvc;
using Services;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CitadelInfoController : ControllerBase
    {
        private readonly IVersionService _versionService;
        private readonly ILogger<CitadelInfoController> _logger;

        public CitadelInfoController(ILogger<CitadelInfoController> logger, IVersionService versionService)
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
