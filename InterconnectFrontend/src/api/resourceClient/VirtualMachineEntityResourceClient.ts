import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import VirtualMachineEntityResponse from "../responses/VirtualMachineEntityResponse.ts";
import VirtualMachinesEntitiesResponse from "../responses/VirtualMachinesEntitiesResponse.ts";
import UpdateEntityPositionRequest from "../requests/UpdateEntityPositionRequest.ts";

class VirtualMachineEntityResourceClient extends BaseBackendResourceClient {
    public async updateEntityPosition(req: UpdateEntityPositionRequest): Promise<VirtualMachineEntityResponse> {
        return await this.sendBackendRequest("UpdateVirtualMachineEntityPosition", "POST", req);
    }

    public async getListOfEntities(): Promise<VirtualMachinesEntitiesResponse> {
        return await this.sendBackendRequest("GetVirtualMachineEntities", "GET", null);
    }

    protected getResourceName(): string {
        return "VirtualMachineEntity";
    }
}

const virtualMachineEntityResourceClient = new VirtualMachineEntityResourceClient();

export { virtualMachineEntityResourceClient };