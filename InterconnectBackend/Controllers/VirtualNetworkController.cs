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
    public sealed class VirtualNetworkController : ControllerBase
    {
        private readonly IVirtualNetworkService _virtualNetworkService;

        public VirtualNetworkController(IVirtualNetworkService virtualNetworkService)
        {
            _virtualNetworkService = virtualNetworkService;
        }

        [HttpGet]
        public async Task<VirtualNetworkEntityConnectionsResponse> GetAllConnections()
        {
            var connections = await _virtualNetworkService.GetVirtualNetworkConnections();

            return VirtualNetworkEntityConnectionsResponse.WithSuccess(connections);
        }

        [HttpPost]
        public async Task<VirtualNetworkEntityConnectionsResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            VirtualNetworkEntityConnectionDTO? virtualNetworkConnection = null;

            if (req.SourceEntityType == EntityType.VirtualMachine && req.DestinationEntityType == EntityType.VirtualMachine)
            {
                virtualNetworkConnection = await _virtualNetworkService.ConnectTwoVirtualMachines(req.SourceEntity.Id, req.SourceSocketId, req.DestinationEntity.Id, req.DestinationSocketId);
            }


            if (virtualNetworkConnection is null)
            {
                throw new NotImplementedException("Unsuported entity types");
            }

            return VirtualNetworkEntityConnectionsResponse.WithSuccess([virtualNetworkConnection]);
        }

        [HttpPost]
        public async Task<VirtualSwitchResponse> CreateVirtualSwitch(CreateVirtualSwitchRequest req)
        {
            var virtualSwitch = await _virtualNetworkService.CreateVirtualSwitch(req.Name);
        
            return VirtualSwitchResponse.WithSuccess(virtualSwitch);
        }
    }
}
