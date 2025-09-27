import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "../requests/CreateVirtualMachineRequest.ts";
import VirtualMachinesEntitiesResponse from "../responses/VirtualMachinesEntitiesResponse.ts";
import CreateVirtualSwitchRequest from "../requests/CreateVirtualSwitchRequest.ts";
import VirtualSwitchesResponse from "../responses/VirtualSwitchesResponse.ts";
import StringResponse from "../responses/StringResponse.ts";
import UpdateEntityPositionRequest from "../requests/UpdateEntityPositionRequest.ts";
import InternetEntitiesResponse from "../responses/InternetEntitiesResponse.ts";

class EntityResourceClient extends BaseBackendResourceClient {
    public createVirtualMachineEntity(
        req: CreateVirtualMachineRequest,
    ): Promise<VirtualMachinesEntitiesResponse> {
        return this.sendBackendRequest(
            "CreateVirtualMachineEntity",
            "POST",
            req,
        );
    }

    public createVirtualSwitchEntity(
        req: CreateVirtualSwitchRequest,
    ): Promise<VirtualSwitchesResponse> {
        return this.sendBackendRequest(
            "CreateVirtualSwitchEntity",
            "POST",
            req,
        );
    }

    public createInternetEntity(): Promise<VirtualSwitchesResponse> {
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

    public getAllVirtualSwitchEntities(): Promise<VirtualSwitchesResponse> {
        return this.sendBackendRequest(
            "GetAllVirtualSwitchEntities",
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
