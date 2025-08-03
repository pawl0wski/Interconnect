import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import BaseResponse from "./responses/BaseResponse.ts";
import { BootableDiskModel } from "../models/BootableDiskModel.ts";

class VirtualMachineManagerResourceClient extends BaseBackendResourceClient {
    public async getAvailableBootableDisks(): Promise<BaseResponse<BootableDiskModel[]>> {
        return this.sendBackendRequest("GetAvailableBootableDisks", "GET", null);
    }

    protected getResourceName(): string {
        return "VirtualMachineManager";
    }
}

const virtualMachineManagerResourceClient = new VirtualMachineManagerResourceClient();

export { virtualMachineManagerResourceClient };