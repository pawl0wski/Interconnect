using Microsoft.AspNetCore.Mvc;
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
        private readonly IEntitiesConnectorService _entitiesConnectorService;
        private readonly IEntitiesDisconnectorService _entitiesDisconnectorService;

        public VirtualNetworkController(
            IVirtualNetworkService virtualNetworkService,
            IEntitiesConnectorService entitiesConnectorService,
            IEntitiesDisconnectorService entitiesDisconnectorService)
        {
            _virtualNetworkService = virtualNetworkService;
            _entitiesConnectorService = entitiesConnectorService;
            _entitiesDisconnectorService = entitiesDisconnectorService;
        }


        [HttpPost]
        public async Task<VirtualNetworkConnectionsResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            var virtualNetworkConnection = await _entitiesConnectorService.ConnectTwoEntities(req.SourceEntityId, req.SourceEntityType, req.DestinationEntityId, req.DestinationEntityType);

            return VirtualNetworkConnectionsResponse.WithSuccess([virtualNetworkConnection]);
        }

        [HttpPost]
        public async Task<StringResponse> DisconnectEntities(VirtualNetworkEntityConnectionRequest req)
        {
            await _entitiesDisconnectorService.DisconnectEntities(req.Id);

            return StringResponse.WithEmptySuccess();
        }

        [HttpGet]
        public async Task<VirtualNetworkConnectionsResponse> GetAllConnections()
        {
            var connections = await _virtualNetworkService.GetVirtualNetworkConnections();

            return VirtualNetworkConnectionsResponse.WithSuccess(connections);
        }
    }
}
