import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import BaseResponse from "./responses/BaseResponse.ts";

class VirtualMachineEntityResourceClient extends BaseBackendResourceClient {
    public async createEntity(name: string, x: number, y: number): Promise<BaseResponse<VirtualMachineEntityModel>> {
        return await this.sendBackendRequest("CreateVirtualMachineEntity", "POST", { name, x, y });
    }

    public async updateEntityPosition(id: number, x: number, y: number): Promise<BaseResponse<VirtualMachineEntityModel>> {
        return await this.sendBackendRequest("UpdateVirtualMachineEntityPosition", "POST", { id, x, y });
    }

    public async getListOfEntities(): Promise<BaseResponse<VirtualMachineEntityModel[]>> {
        return await this.sendBackendRequest("GetVirtualMachineEntities", "GET", null);
    }

    protected getResourceName(): string {
        return "VirtualMachineEntity";
    }
}

const virtualMachineEntityResourceClient = new VirtualMachineEntityResourceClient();

export { virtualMachineEntityResourceClient };