import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectEntitiesRequest from "../requests/ConnectEntitiesRequest.ts";
import VirtualNetworkConnectionsResponse from "../responses/VirtualNetworkConnectionsResponse.ts";
import VirtualNetworkEntityConnectionRequest from "../requests/VirtualNetworkEntityConnectionRequest.ts";
import StringResponse from "../responses/StringResponse.ts";

class VirtualNetworkResourceClient extends BaseBackendResourceClient {
    public connectEntities(request: ConnectEntitiesRequest) {
        return this.sendBackendRequest("ConnectEntities", "POST", request);
    }

    public getAllConnections(): Promise<VirtualNetworkConnectionsResponse> {
        return this.sendBackendRequest("GetAllConnections", "GET", {});
    }

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
