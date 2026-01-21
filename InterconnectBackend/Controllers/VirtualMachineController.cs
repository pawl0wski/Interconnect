using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller managing virtual machines.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualMachineController : ControllerBase
    {
        private readonly IBootableDiskProviderService _bootableDiskProviderService;

        public VirtualMachineController(IBootableDiskProviderService bootableDiskProviderService)
        {
            _bootableDiskProviderService = bootableDiskProviderService;
        }

        /// <summary>
        /// Retrieves a list of available bootable disks.
        /// </summary>
        /// <returns>List of bootable disks.</returns>
        [HttpGet]
        public async Task<ActionResult<BootableDisksResponse>> GetAvailableBootableDisks()
        {
            var bootableDisks = await _bootableDiskProviderService.GetAvailableBootableDiskModels();

            return Ok(BootableDisksResponse.WithSuccess(bootableDisks));
        }
    }
}
