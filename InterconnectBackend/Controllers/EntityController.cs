using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class EntityController : ControllerBase
    {
        private readonly IVirtualMachineEntityService _vmEntityService;
        private readonly IVirtualMachineManagerService _vmManagerService;
        private readonly IBootableDiskProviderService _bootableDiskProviderService;
        private readonly IVirtualNetworkService _virtualNetworkService;
        private readonly IInternetEntityService _internetEntityService;

        public EntityController(
            IVirtualMachineEntityService vmEntityService,
            IVirtualMachineManagerService vmManagerService,
            IBootableDiskProviderService bootableDiskProviderService,
            IVirtualNetworkService virtualNetworkService,
            IInternetEntityService internetEntityService)
        {
            _vmEntityService = vmEntityService;
            _vmManagerService = vmManagerService;
            _bootableDiskProviderService = bootableDiskProviderService;
            _virtualNetworkService = virtualNetworkService;
            _internetEntityService = internetEntityService;
        }

        [HttpPost]
        public async Task<ActionResult<VirtualMachineEntitiesResponse>> CreateVirtualMachineEntity(CreateVirtualMachineEntityRequest req)
        {
            var entity = await _vmEntityService.CreateEntity(req.Name, req.BootableDiskId, req.Memory, req.VirtualCPUs, 25, 25);

            return Ok(VirtualMachineEntitiesResponse.WithSuccess([entity]));
        }

        [HttpPost]
        public async Task<ActionResult<VirtualSwitchesEntitiesResponse>> CreateVirtualSwitchEntity(CreateVirtualSwitchEntityRequest req)
        {
            var virtualSwitch = await _virtualNetworkService.CreateVirtualSwitch(req.Name);

            return VirtualSwitchesEntitiesResponse.WithSuccess([virtualSwitch]);
        }

        [HttpPost]
        public async Task<ActionResult<InternetEntitiesResponse>> CreateInternetEntity()
        {
            var internetEntity = await _internetEntityService.CreateInternet();

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        [HttpPut]
        public async Task<ActionResult<StringResponse>> UpdateEntityPosition(UpdateEntityPositionRequest req)
        {
            switch (req.Type)
            {
                case EntityType.VirtualMachine:
                    await _vmEntityService.UpdateEntityPosition(req.Id, req.X, req.Y);
                    break;
                case EntityType.VirtualSwitch:
                    await _virtualNetworkService.UpdateVirtualSwitchEntityPosition(req.Id, req.X, req.Y);
                    break;
                case EntityType.Internet:
                    await _internetEntityService.UpdateInternetEntityPosition(req.Id, req.X, req.Y);
                    break;
                default:
                    throw new Exception("Unsuported entity type");
            }

            return Ok(StringResponse.WithSuccess("OK"));
        }

        [HttpGet]
        public async Task<ActionResult<VirtualMachineEntitiesResponse>> GetAllVirtualMachineEntities()
        {
            var entities = await _vmEntityService.GetEntities();

            return Ok(VirtualMachineEntitiesResponse.WithSuccess(entities));
        }

        [HttpGet]
        public async Task<ActionResult<VirtualSwitchesEntitiesResponse>> GetAllVirtualSwitchEntities()
        {
            var entities = await _virtualNetworkService.GetVisibleVirtualSwitchEntities();

            return Ok(VirtualSwitchesEntitiesResponse.WithSuccess(entities));
        }

        [HttpGet]
        public async Task<ActionResult<InternetEntitiesResponse>> GetAllInternetEntities()
        {
            var entities = await _internetEntityService.GetInternetEntities();

            return Ok(InternetEntitiesResponse.WithSuccess(entities));
        }
    }
}
