import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectEntitiesRequest from "../requests/ConnectEntitiesRequest.ts";
import VirtualNetworkConnectionsResponse from "../responses/VirtualNetworkConnectionsResponse.ts";
import VirtualSwitchesResponse from "../responses/VirtualSwitchesResponse.ts";
import CreateVirtualSwitchRequest from "../requests/CreateVirtualSwitchRequest.ts";
import UpdateEntityPositionRequest from "../requests/UpdateEntityPositionRequest.ts";
import InternetEntitiesResponse from "../responses/InternetEntitiesResponse.ts";

class VirtualNetworkResourceClient extends BaseBackendResourceClient {
    public connectEntities(request: ConnectEntitiesRequest) {
        return this.sendBackendRequest("ConnectEntities", "POST", request);
    }

    public getAllConnections(): Promise<VirtualNetworkConnectionsResponse> {
        return this.sendBackendRequest("GetAllConnections", "GET", {});
    }

    public createVirtualSwitch(req: CreateVirtualSwitchRequest): Promise<VirtualSwitchesResponse> {
        return this.sendBackendRequest("CreateVirtualSwitch", "POST", req);
    }

    public updateVirtualSwitchEntityPosition(req: UpdateEntityPositionRequest): Promise<VirtualSwitchesResponse> {
        return this.sendBackendRequest("UpdateVirtualSwitchEntityPosition", "POST", req);
    }

    public getVirtualSwitchEntities(): Promise<VirtualSwitchesResponse> {
        return this.sendBackendRequest("GetVirtualSwitchEntities", "GET", {});
    }

    public createInternet(): Promise<InternetEntitiesResponse> {
        return this.sendBackendRequest("CreateInternet", "POST", {});
    }

    public getInternetEntities(): Promise<InternetEntitiesResponse> {
        return this.sendBackendRequest("GetInternetEntities", "GET", {});
    }

    public updateInternetEntityPosition(req: UpdateEntityPositionRequest): Promise<InternetEntitiesResponse> {
        return this.sendBackendRequest("UpdateInternetEntityPosition", "POST", req);
    }

    protected getResourceName(): string {
        return "VirtualNetwork";
    }
}

const virtualNetworkResourceClient = new VirtualNetworkResourceClient();

export default virtualNetworkResourceClient;

