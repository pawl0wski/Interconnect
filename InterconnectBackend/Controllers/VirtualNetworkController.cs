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
            VirtualNetworkConnectionDTO? virtualNetworkConnection = null;

            if (AreTypes(req.SourceEntityType, req.DestinationEntityType, EntityType.VirtualMachine, EntityType.VirtualMachine))
            {
                virtualNetworkConnection = await _virtualNetworkService.ConnectTwoVirtualMachines(req.SourceEntityId, req.DestinationEntityId);
            }

            if (AreTypes(req.SourceEntityType, req.DestinationEntityType, EntityType.VirtualSwitch, EntityType.VirtualMachine))
            {
                var (sourceEntityId, destinationEntityId) = ResolveEntityIdsOrder(req);

                virtualNetworkConnection = await _virtualNetworkService.ConnectVirtualMachineToVirtualSwitch(sourceEntityId, destinationEntityId);
            }

            if (AreTypes(req.SourceEntityType, req.DestinationEntityType, EntityType.Internet, EntityType.VirtualMachine))
            {
                var (sourceEntityId, destinationEntityId) = ResolveEntityIdsOrder(req);

                virtualNetworkConnection = await _virtualNetworkService.ConnectVirtualMachineToInternet(sourceEntityId, destinationEntityId);
            }


            if (virtualNetworkConnection is null)
            {
                throw new NotImplementedException("Unsuported entity types");
            }

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

        private (int sourceEntityId, int destinationEntityId) ResolveEntityIdsOrder(ConnectEntitiesRequest req)
        {
            var sourceEntityId = req.SourceEntityId;
            var destinationEntityId = req.DestinationEntityId;

            if (req.SourceEntityType != EntityType.VirtualMachine)
            {
                (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
            }

            return (sourceEntityId, destinationEntityId);
        }

        private bool AreTypes(EntityType sourceEntityType, EntityType destinationEntityType, EntityType firstEntityType, EntityType secondEntityType)
        {
            return (sourceEntityType == firstEntityType && destinationEntityType == secondEntityType) || (sourceEntityType == secondEntityType && destinationEntityType == firstEntityType);
        }
    }
}
