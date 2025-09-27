import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import BootableDisksResponse from "../responses/BootableDisksResponse.ts";

class VirtualMachineResourceClient extends BaseBackendResourceClient {
    public async getAvailableBootableDisks(): Promise<BootableDisksResponse> {
        return this.sendBackendRequest(
            "GetAvailableBootableDisks",
            "GET",
            null,
        );
    }

    protected getResourceName(): string {
        return "VirtualMachine";
    }
}

const virtualMachineResourceClient = new VirtualMachineResourceClient();

export { virtualMachineResourceClient };
