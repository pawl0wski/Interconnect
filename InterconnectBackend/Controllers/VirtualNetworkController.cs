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
                var sourceEntityId = req.SourceEntityId;
                var destinationEntityId = req.DestinationEntityId;

                if (req.SourceEntityType != EntityType.VirtualMachine)
                {
                    (sourceEntityId, destinationEntityId) = (destinationEntityId, sourceEntityId);
                }

                virtualNetworkConnection = await _virtualNetworkService.ConnectVirtualMachineToVirtualSwitch(sourceEntityId, destinationEntityId);
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

        private bool AreTypes(EntityType sourceEntityType, EntityType destinationEntityType, EntityType firstEntityType, EntityType secondEntityType)
        {
            return (sourceEntityType == firstEntityType && destinationEntityType == secondEntityType) || (sourceEntityType == secondEntityType && destinationEntityType == firstEntityType);
        }
    }
}
