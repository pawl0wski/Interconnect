using Microsoft.AspNetCore.Mvc;
using Models.Requests;
using Models.Responses;
using Services;

namespace Controllers
{
    /// <summary>
    /// Controller managing virtual networks and connections.
    /// </summary>
    [ApiController]
    [Route("[controller]/[action]")]
    public sealed class VirtualNetworkController : ControllerBase
    {
        private readonly IVirtualNetworkService _virtualNetworkService;
        private readonly IEntitiesConnectorService _entitiesConnectorService;
        private readonly IEntitiesDisconnectorService _entitiesDisconnectorService;

        /// <summary>
        /// Initializes a new instance of the VirtualNetworkController.
        /// </summary>
        /// <param name="virtualNetworkService">Service for managing virtual networks.</param>
        /// <param name="entitiesConnectorService">Service for connecting entities.</param>
        /// <param name="entitiesDisconnectorService">Service for disconnecting entities.</param>
        public VirtualNetworkController(
            IVirtualNetworkService virtualNetworkService,
            IEntitiesConnectorService entitiesConnectorService,
            IEntitiesDisconnectorService entitiesDisconnectorService)
        {
            _virtualNetworkService = virtualNetworkService;
            _entitiesConnectorService = entitiesConnectorService;
            _entitiesDisconnectorService = entitiesDisconnectorService;
        }


        /// <summary>
        /// Connects two entities in the virtual network.
        /// </summary>
        /// <param name="req">Connection data.</param>
        /// <returns>Created connection.</returns>
        [HttpPost]
        public async Task<VirtualNetworkConnectionsResponse> ConnectEntities(ConnectEntitiesRequest req)
        {
            var virtualNetworkConnection = await _entitiesConnectorService.ConnectTwoEntities(req.SourceEntityId, req.SourceEntityType, req.DestinationEntityId, req.DestinationEntityType);

            return VirtualNetworkConnectionsResponse.WithSuccess([virtualNetworkConnection]);
        }

        /// <summary>
        /// Disconnects entities in the virtual network.
        /// </summary>
        /// <param name="req">Connection data to remove.</param>
        /// <returns>Operation confirmation.</returns>
        [HttpPost]
        public async Task<StringResponse> DisconnectEntities(VirtualNetworkEntityConnectionRequest req)
        {
            await _entitiesDisconnectorService.DisconnectEntities(req.Id);

            return StringResponse.WithEmptySuccess();
        }

        /// <summary>
        /// Retrieves all connections in the virtual network.
        /// </summary>
        /// <returns>List of connections.</returns>
        [HttpGet]
        public async Task<VirtualNetworkConnectionsResponse> GetAllConnections()
        {
            var connections = await _virtualNetworkService.GetVirtualNetworkConnections();

            return VirtualNetworkConnectionsResponse.WithSuccess(connections);
        }
    }
}
