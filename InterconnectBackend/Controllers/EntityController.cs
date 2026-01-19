using Microsoft.AspNetCore.Mvc;
using Models.DTO;
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
        private readonly IVirtualNetworkService _virtualNetworkService;
        private readonly IInternetEntityService _internetEntityService;

        public EntityController(
            IVirtualMachineEntityService vmEntityService,
            IVirtualNetworkService virtualNetworkService,
            IInternetEntityService internetEntityService)
        {
            _vmEntityService = vmEntityService;
            _virtualNetworkService = virtualNetworkService;
            _internetEntityService = internetEntityService;
        }

        [HttpPost]
        public async Task<ActionResult<VirtualMachineEntityResponse>> CreateVirtualMachineEntity(CreateVirtualMachineEntityRequest req)
        {
            var entity = await _vmEntityService.CreateEntity(req.Name, req.BootableDiskId, req.Memory, req.VirtualCPUs, req.Type, 25, 25);

            return Ok(VirtualMachineEntityResponse.WithSuccess(entity));
        }

        [HttpPost]
        public async Task<ActionResult<VirtualNetworkNodesEntitiesResponse>> CreateVirtualNetworkNodeEntity(CreateVirtualNetworkNodeEntityRequest req)
        {
            var virtualNetworkNode = await _virtualNetworkService.CreateVirtualNetworkNode(req.Name);

            return VirtualNetworkNodesEntitiesResponse.WithSuccess([virtualNetworkNode]);
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
                case EntityType.VirtualNetworkNode:
                    await _virtualNetworkService.UpdateVirtualNetworkNodeEntityPosition(req.Id, req.X, req.Y);
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
            var macAddresses = await _vmEntityService.GetMacAddresses();

            return Ok(VirtualMachineEntitiesResponse.WithSuccess(
                new VirtualMachineEntitiesWithMacAddressesDTO
                {
                    VirtualMachineEntities = entities,
                    MacAddressEntities = macAddresses
                }
                ));
        }

        [HttpGet]
        public async Task<ActionResult<VirtualNetworkNodesEntitiesResponse>> GetAllVirtualNetworkNodeEntities()
        {
            var entities = await _virtualNetworkService.GetVisibleVirtualNetworkNodeEntities();

            return Ok(VirtualNetworkNodesEntitiesResponse.WithSuccess(entities));
        }

        [HttpGet]
        public async Task<ActionResult<InternetEntitiesResponse>> GetAllInternetEntities()
        {
            var entities = await _internetEntityService.GetInternetEntities();

            return Ok(InternetEntitiesResponse.WithSuccess(entities));
        }
    }
}
