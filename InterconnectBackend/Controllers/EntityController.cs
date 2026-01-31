using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Enums;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller managing entities in the system.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class EntityController : ControllerBase
    {
        private readonly IVirtualMachineEntityService _vmEntityService;
        private readonly IVirtualNetworkService _virtualNetworkService;
        private readonly IInternetEntityService _internetEntityService;
        private readonly IDeleteEntityService _deleteEntityService;

        public EntityController(
            IVirtualMachineEntityService vmEntityService,
            IVirtualNetworkService virtualNetworkService,
            IInternetEntityService internetEntityService,
            IDeleteEntityService deleteEntityService)
        {
            _vmEntityService = vmEntityService;
            _virtualNetworkService = virtualNetworkService;
            _internetEntityService = internetEntityService;
            _deleteEntityService = deleteEntityService;
        }

        /// <summary>
        /// Creates a new virtual machine entity.
        /// </summary>
        /// <param name="req">Data for creating virtual machine.</param>
        /// <returns>Created virtual machine entity.</returns>
        [HttpPost]
        public async Task<ActionResult<VirtualMachineEntityResponse>> CreateVirtualMachineEntity(CreateVirtualMachineEntityRequest req)
        {
            var entity = await _vmEntityService.CreateEntity(req.Name, req.BootableDiskId, req.Memory, req.VirtualCPUs, req.Type, 25, 25);

            return Ok(VirtualMachineEntityResponse.WithSuccess(entity));
        }

        /// <summary>
        /// Creates a new virtual network node.
        /// </summary>
        /// <param name="req">Data for creating node.</param>
        /// <returns>Created network node.</returns>
        [HttpPost]
        public async Task<ActionResult<VirtualNetworkNodesEntitiesResponse>> CreateVirtualNetworkNodeEntity(CreateVirtualNetworkNodeEntityRequest req)
        {
            var virtualNetworkNode = await _virtualNetworkService.CreateVirtualNetworkNode(req.Name);

            return VirtualNetworkNodesEntitiesResponse.WithSuccess([virtualNetworkNode]);
        }

        /// <summary>
        /// Creates a new Internet entity.
        /// </summary>
        /// <returns>Created Internet entity.</returns>
        [HttpPost]
        public async Task<ActionResult<InternetEntitiesResponse>> CreateInternetEntity()
        {
            var internetEntity = await _internetEntityService.CreateInternet();

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        /// <summary>
        /// Updates entity position on the board.
        /// </summary>
        /// <param name="req">Data for updating position.</param>
        /// <returns>Operation confirmation.</returns>
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

        [HttpDelete]
        public async Task<ActionResult<StringResponse>> DeleteEntity(DeleteEntityRequest request)
        {
            switch (request.Type)
            {
                case EntityType.VirtualMachine:
                    await _deleteEntityService.DeleteVirtualMachineEntity(request.Id);
                    break;
                case EntityType.VirtualNetworkNode:
                    await _deleteEntityService.DeleteVirtualNetworkNodeEntity(request.Id);
                    break;
                case EntityType.Internet:
                    await _deleteEntityService.DeleteInternetEntity(request.Id);
                    break;
                default:
                    throw new Exception("Unsuported entity type");
            }

            return Ok(StringResponse.WithEmptySuccess());
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
