import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectEntitiesRequest from "../requests/ConnectEntitiesRequest.ts";
import VirtualNetworkEntityConnectionsResponse from "../responses/VirtualNetworkEntityConnectionsResponse.ts";

class VirtualNetworkResourceClient extends BaseBackendResourceClient {
    public async connectEntities(request: ConnectEntitiesRequest) {
        return this.sendBackendRequest("ConnectEntities", "POST", request);
    }

    public async getAllConnections(): Promise<VirtualNetworkEntityConnectionsResponse> {
        return this.sendBackendRequest("GetAllConnections", "GET", {});
    }

    protected getResourceName(): string {
        return "VirtualNetwork";
    }
}

const virtualNetworkResourceClient = new VirtualNetworkResourceClient();

export default virtualNetworkResourceClient;

