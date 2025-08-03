import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "./requests/CreateVirtualMachineRequest.ts";
import BaseResponse from "./responses/BaseResponse.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";
import { BootableDiskModel } from "../models/BootableDiskModel.ts";

class VirtualMachineResourceClient extends BaseBackendResourceClient {
    public createVirtualMachine(request: CreateVirtualMachineRequest): Promise<BaseResponse<VirtualMachineEntityModel>> {
        return this.sendBackendRequest("CreateVirtualMachine", "POST", request);
    }

    public async getAvailableBootableDisks(): Promise<BaseResponse<BootableDiskModel[]>> {
        return this.sendBackendRequest("GetAvailableBootableDisks", "GET", null);
    }

    protected getResourceName(): string {
        return "VirtualMachine";
    }
}

const virtualMachineResourceClient = new VirtualMachineResourceClient();

export { virtualMachineResourceClient };