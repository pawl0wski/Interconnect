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
        public async Task<VirtualNetworkConnectionsResponse> GetAllConnections()
        {
            var connections = await _virtualNetworkService.GetVirtualNetworkConnections();

            return VirtualNetworkConnectionsResponse.WithSuccess(connections);
        }

        [HttpPost]
        public async Task<VirtualNetworkConnectionsResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            var virtualNetworkConnection = await _virtualNetworkService.ConnectTwoEntities(req.SourceEntityId, req.SourceEntityType, req.DestinationEntityId, req.DestinationEntityType);

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
            var internetEntity = await _virtualNetworkService.CreateInternet();

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        [HttpGet]
        public async Task<InternetEntitiesResponse> GetInternetEntities()
        {
            var internetEntities = await _virtualNetworkService.GetInternetEntities();

            return InternetEntitiesResponse.WithSuccess(internetEntities);
        }

        [HttpPost]
        public async Task<InternetEntitiesResponse> UpdateInternetEntityPosition(UpdateEntityPositionRequest req)
        {
            var internetEntity = await _virtualNetworkService.UpdateInternetEntityPosition(req.Id, req.X, req.Y);

            return InternetEntitiesResponse.WithSuccess([internetEntity]);
        }

        [HttpPost]
        public async Task<StringResponse> DisconnectEntities(VirtualNetworkEntityConnectionRequest req)
        {
            await _virtualNetworkService.DisconnectEntities(req.Id);

            return StringResponse.WithEmptySuccess();
        }
    }
}
