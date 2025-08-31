using Microsoft.AspNetCore.Mvc;
using Models.Enums;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualNetworkController : ControllerBase
    {
        private readonly IVirtualNetworkService _virtualNetworkService;

        public VirtualNetworkController(IVirtualNetworkService virtualNetworkService)
        {
            _virtualNetworkService = virtualNetworkService;
        }

        public async Task<ConnectEntitiesResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            if (req.SourceEntityType == EntityType.VirtualMachine && req.DestinationEntityType == EntityType.VirtualMachine)
            {
                await _virtualNetworkService.ConnectTwoVirtualMachines(req.SourceEntity.Id, req.SourceSocketId, req.DestinationEntity.Id, req.DestinationSocketId);
            }

            return ConnectEntitiesResponse.WithSuccess("Siema");
        }
    }
}
