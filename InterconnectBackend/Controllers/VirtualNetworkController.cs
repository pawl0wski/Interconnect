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
        private readonly IInternetEntityService _internetEntityService;

        public VirtualNetworkController(
            IVirtualNetworkService virtualNetworkService,
            IEntitiesConnectorService entitiesConnectorService,
            IEntitiesDisconnectorService entitiesDisconnectorService,
            IInternetEntityService internetEntityService)
        {
            _virtualNetworkService = virtualNetworkService;
            _entitiesConnectorService = entitiesConnectorService;
            _entitiesDisconnectorService = entitiesDisconnectorService;
            _internetEntityService = internetEntityService;
        }

        [HttpGet]
        public async Task<VirtualNetworkConnectionsResponse> GetAllConnections()
        {
            var connections = await _virtualNetworkService.GetVirtualNetworkConnections();

            return VirtualNetworkConnectionsResponse.WithSuccess(connections);
        }

        [HttpPost]
        public async Task<VirtualNetworkConnectionsResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            var virtualNetworkConnection = await _entitiesConnectorService.ConnectTwoEntities(req.SourceEntityId, req.SourceEntityType, req.DestinationEntityId, req.DestinationEntityType);

            return VirtualNetworkConnectionsResponse.WithSuccess([virtualNetworkConnection]);
        }

        [HttpPost]
        public async Task<VirtualSwitchesEntitiesResponse> CreateVirtualSwitch(CreateVirtualSwitchRequest req)
        {
            var virtualSwitch = await _virtualNetworkService.CreateVirtualSwitch(req.Name);

            return VirtualSwitchesEntitiesResponse.WithSuccess([virtualSwitch]);
        }

        [HttpPost]
        public async Task<VirtualSwitchesEntitiesResponse> UpdateVirtualSwitchEntityPosition(UpdateEntityPositionRequest req)
        {
            var virtualSwitch = await _virtualNetworkService.UpdateVirtualSwitchEntityPosition(req.Id, req.X, req.Y);

            return VirtualSwitchesEntitiesResponse.WithSuccess([virtualSwitch]);
        }

        [HttpGet]
        public async Task<VirtualSwitchesEntitiesResponse> GetVirtualSwitchEntities()
        {
            var virtualSwitches = await _virtualNetworkService.GetVisibleVirtualSwitchEntities();

            return VirtualSwitchesEntitiesResponse.WithSuccess(virtualSwitches);
        }

        [HttpPost]
        public async Task<InternetEntitiesResponse> CreateInternet()
        {
            var internetEntity = await _internetEntityService.CreateInternet();

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        [HttpGet]
        public async Task<InternetEntitiesResponse> GetInternetEntities()
        {
            var internetEntities = await _internetEntityService.GetInternetEntities();

            return InternetEntitiesResponse.WithSuccess(internetEntities);
        }

        [HttpPost]
        public async Task<InternetEntitiesResponse> UpdateInternetEntityPosition(UpdateEntityPositionRequest req)
        {
            var internetEntity = await _internetEntityService.UpdateInternetEntityPosition(req.Id, req.X, req.Y);

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        [HttpPost]
        public async Task<StringResponse> DisconnectEntities(VirtualNetworkEntityConnectionRequest req)
        {
            await _entitiesDisconnectorService.DisconnectEntities(req.Id);

            return StringResponse.WithEmptySuccess();
        }
    }
}
