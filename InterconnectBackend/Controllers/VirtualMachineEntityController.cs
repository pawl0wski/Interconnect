using Controllers.Requests;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
        public async Task<ActionResult<BaseResponse<VirtualMachineEntityDTO>>> CreateVirtualMachineEntity(CreateVirtualMachineEntityRequest req)
        {
            var entity = await _entityService.CreateEntity(req.Name, req.X, req.Y);

            return Ok(BaseResponse<VirtualMachineEntityDTO>.WithSuccess(entity));
        }

        [HttpPost]
        public async Task<ActionResult<BaseResponse<VirtualMachineEntityDTO>>> UpdateVirtualMachineEntityPosition(UpdateVirtualMachineEntityPositionRequest req)
        {
            var entity = await _entityService.UpdateEntityPosition(req.Id, req.X, req.Y);

            return Ok(BaseResponse<VirtualMachineEntityDTO>.WithSuccess(entity));
        }

        [HttpGet]
        public async Task<ActionResult<BaseResponse<List<VirtualMachineEntityDTO>>>> GetVirtualMachineEntities()
        {
            var entities = await _entityService.GetEntities();

            return Ok(BaseResponse<List<VirtualMachineEntityDTO>>.WithSuccess(entities));
        }
    }
}
