import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "../requests/CreateVirtualMachineRequest.ts";
import VirtualMachinesEntitiesResponse from "../responses/VirtualMachinesEntitiesResponse.ts";
import CreateVirtualNetworkNodeRequest from "../requests/CreateVirtualNetworkNodeRequest.ts";
import VirtualNetworkNodesResponse from "../responses/VirtualNetworkNodesResponse.ts";
import StringResponse from "../responses/StringResponse.ts";
import UpdateEntityPositionRequest from "../requests/UpdateEntityPositionRequest.ts";
import InternetEntitiesResponse from "../responses/InternetEntitiesResponse.ts";
import VirtualMachineEntityResponse from "../responses/VirtualMachineEntityResponse.ts";

class EntityResourceClient extends BaseBackendResourceClient {
    public createVirtualMachineEntity(
        req: CreateVirtualMachineRequest,
    ): Promise<VirtualMachineEntityResponse> {
        return this.sendBackendRequest(
            "CreateVirtualMachineEntity",
            "POST",
            req,
        );
    }

    public createVirtualNetworkNodeEntity(
        req: CreateVirtualNetworkNodeRequest,
    ): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest(
            "CreateVirtualNetworkNodeEntity",
            "POST",
            req,
        );
    }

    public createInternetEntity(): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest("CreateInternetEntity", "POST", {});
    }

    public updateEntityPosition(
        req: UpdateEntityPositionRequest,
    ): Promise<StringResponse> {
        return this.sendBackendRequest("UpdateEntityPosition", "PUT", req);
    }

    public getAllVirtualMachineEntities(): Promise<VirtualMachinesEntitiesResponse> {
        return this.sendBackendRequest(
            "GetAllVirtualMachineEntities",
            "GET",
            {},
        );
    }

    public getAllVirtualNetworkNodeEntities(): Promise<VirtualNetworkNodesResponse> {
        return this.sendBackendRequest(
            "GetAllVirtualNetworkNodeEntities",
            "GET",
            {},
        );
    }

    public getAllInternetEntities(): Promise<InternetEntitiesResponse> {
        return this.sendBackendRequest("GetAllInternetEntities", "GET", {});
    }

    protected getResourceName(): string {
        return "Entity";
    }
}

const instance = new EntityResourceClient();

export default instance;
