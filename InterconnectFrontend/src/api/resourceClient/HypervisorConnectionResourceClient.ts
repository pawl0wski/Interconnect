import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import ConnectionInfoResponse from "../responses/ConnectionInfoResponse.ts";

class HypervisorConnectionResourceClient extends BaseBackendResourceClient {
    public async connectionInfo(): Promise<ConnectionInfoResponse> {
        return await this.sendBackendRequest("ConnectionInfo", "GET", {});
    }

    protected getResourceName(): string {
        return "HypervisorConnection";
    }
}

const hypervisorConnectionClient = new HypervisorConnectionResourceClient();

export { hypervisorConnectionClient };
