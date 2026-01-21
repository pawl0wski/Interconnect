import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectEntitiesRequest from "../requests/ConnectEntitiesRequest.ts";
import VirtualNetworkConnectionsResponse from "../responses/VirtualNetworkConnectionsResponse.ts";
import VirtualNetworkEntityConnectionRequest from "../requests/VirtualNetworkEntityConnectionRequest.ts";
import StringResponse from "../responses/StringResponse.ts";

/**
 * Resource client for virtual network and connection management API operations.
 */
class VirtualNetworkResourceClient extends BaseBackendResourceClient {
    /**
     * Establishes a connection between two entities in the virtual network.
     * @param {ConnectEntitiesRequest} request The connection request with source and destination entities
     * @returns {Promise<VirtualNetworkConnectionsResponse>} Response containing the new connection
     */
    public connectEntities(request: ConnectEntitiesRequest) {
        return this.sendBackendRequest("ConnectEntities", "POST", request);
    }

    /**
     * Retrieves all virtual network connections.
     * @returns {Promise<VirtualNetworkConnectionsResponse>} List of all network connections
     */
    public getAllConnections(): Promise<VirtualNetworkConnectionsResponse> {
        return this.sendBackendRequest("GetAllConnections", "GET", {});
    }

    /**
     * Disconnects two entities in the virtual network.
     * @param {VirtualNetworkEntityConnectionRequest} req The disconnection request
     * @returns {Promise<StringResponse>} Success message response
     */
    public disconnectEntities(
        req: VirtualNetworkEntityConnectionRequest,
    ): Promise<StringResponse> {
        return this.sendBackendRequest("DisconnectEntities", "POST", req);
    }

    protected getResourceName(): string {
        return "VirtualNetwork";
    }
}

const virtualNetworkResourceClient = new VirtualNetworkResourceClient();

export default virtualNetworkResourceClient;
