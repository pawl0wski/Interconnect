import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectEntitiesRequest from "../requests/ConnectEntitiesRequest.ts";

class VirtualNetworkResourceClient extends BaseBackendResourceClient {
    public async connectEntities(request: ConnectEntitiesRequest) {
        return this.sendBackendRequest("ConnectEntities", "POST", request);
    }

    protected getResourceName(): string {
        return "VirtualNetwork";
    }
}

const virtualNetworkResourceClient = new VirtualNetworkResourceClient();

export default virtualNetworkResourceClient;

