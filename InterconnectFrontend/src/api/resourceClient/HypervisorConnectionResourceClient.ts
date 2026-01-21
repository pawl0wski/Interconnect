import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectionInfoResponse from "../responses/ConnectionInfoResponse.ts";

/**
 * Resource client for hypervisor connection-related API operations.
 */
class HypervisorConnectionResourceClient extends BaseBackendResourceClient {
    /**
     * Retrieves connection information and system details from the hypervisor.
     * @returns {Promise<ConnectionInfoResponse>} Connection info including CPU, memory, and driver details
     */
    public async connectionInfo(): Promise<ConnectionInfoResponse> {
        return await this.sendBackendRequest("ConnectionInfo", "GET", {});
    }

    protected getResourceName(): string {
        return "HypervisorConnection";
    }
}

const hypervisorConnectionClient = new HypervisorConnectionResourceClient();

export { hypervisorConnectionClient };
