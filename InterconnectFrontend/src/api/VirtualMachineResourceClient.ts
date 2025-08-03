import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "./requests/CreateVirtualMachineRequest.ts";
import BaseResponse from "./responses/BaseResponse.ts";
import { VirtualMachineEntityModel } from "../models/VirtualMachineEntityModel.ts";

class VirtualMachineResourceClient extends BaseBackendResourceClient {
    public createVirtualMachine(request: CreateVirtualMachineRequest): Promise<BaseResponse<VirtualMachineEntityModel>> {
        return this.sendBackendRequest("CreateVirtualMachine", "POST", request);
    }

    protected getResourceName(): string {
        return "VirtualMachine";
    }
}

const virtualMachineResourceClient = new VirtualMachineResourceClient();

export { virtualMachineResourceClient };