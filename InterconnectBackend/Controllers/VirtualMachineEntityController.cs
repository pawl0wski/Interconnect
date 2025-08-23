using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualMachineEntityController : ControllerBase
    {
        private readonly IVirtualMachineEntityService _entityService;

        public VirtualMachineEntityController(IVirtualMachineEntityService entityService)
        {
            _entityService = entityService;
        }

        [HttpPost]
        public async Task<ActionResult<VirtualMachineEntityResponse>> UpdateVirtualMachineEntityPosition(UpdateVirtualMachineEntityPositionRequest req)
        {
            var entity = await _entityService.UpdateEntityPosition(req.Id, req.X, req.Y);

            return Ok(VirtualMachineEntityResponse.WithSuccess(entity));
        }

        [HttpGet]
        public async Task<ActionResult<VirtualMachinesEntitiesResponse>> GetVirtualMachineEntities()
        {
            var entities = await _entityService.GetEntities();

            return Ok(VirtualMachinesEntitiesResponse.WithSuccess(entities));
        }
    }
}
