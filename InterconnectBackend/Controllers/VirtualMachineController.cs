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

        /// <summary>
        /// Initializes a new instance of the VirtualMachineController.
        /// </summary>
        /// <param name="bootableDiskProviderService">Service for providing bootable disk information.</param>
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
