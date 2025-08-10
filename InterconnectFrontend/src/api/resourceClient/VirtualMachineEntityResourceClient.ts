import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import VirtualMachineEntityResponse from "../responses/VirtualMachineEntityResponse.ts";
import VirtualMachinesEntitiesResponse from "../responses/VirtualMachinesEntitiesResponse.ts";

class VirtualMachineEntityResourceClient extends BaseBackendResourceClient {
    public async updateEntityPosition(id: number, x: number, y: number): Promise<VirtualMachineEntityResponse> {
        return await this.sendBackendRequest("UpdateVirtualMachineEntityPosition", "POST", { id, x, y });
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