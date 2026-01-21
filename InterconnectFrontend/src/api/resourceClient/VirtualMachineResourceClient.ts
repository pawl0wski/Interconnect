import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import BootableDisksResponse from "../responses/BootableDisksResponse.ts";

/**
 * Resource client for virtual machine-related API operations.
 */
class VirtualMachineResourceClient extends BaseBackendResourceClient {
    /**
     * Retrieves all available bootable disks from the backend.
     * @returns {Promise<BootableDisksResponse>} List of available bootable disks
     */
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
