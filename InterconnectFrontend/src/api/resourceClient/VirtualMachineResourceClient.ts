import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import CreateVirtualMachineRequest from "../requests/CreateVirtualMachineRequest.ts";
import VirtualMachineEntityResponse from "../responses/VirtualMachineEntityResponse.ts";
import BootableDisksResponse from "../responses/BootableDisksResponse.ts";

class VirtualMachineResourceClient extends BaseBackendResourceClient {
    public createVirtualMachine(request: CreateVirtualMachineRequest): Promise<VirtualMachineEntityResponse> {
        return this.sendBackendRequest("CreateVirtualMachine", "POST", request);
    }

    public async getAvailableBootableDisks(): Promise<BootableDisksResponse> {
        return this.sendBackendRequest("GetAvailableBootableDisks", "GET", null);
    }

    protected getResourceName(): string {
        return "VirtualMachine";
    }
}

const virtualMachineResourceClient = new VirtualMachineResourceClient();

export { virtualMachineResourceClient };