import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import { InitializeConnectionResponse } from "./responses/InitializeConnectionResponse.ts";
import { ConnectionStatusResponse } from "./responses/ConnectionStatusResponse.ts";

class HypervisorConnectionResourceClient extends BaseBackendResourceClient {
    public async initializeConnection(connectionUrl: string | null): Promise<InitializeConnectionResponse> {
        return await this.sendBackendRequest("InitializeConnection", "POST", { connectionUrl });
    }

    public async connectionStatus(): Promise<ConnectionStatusResponse> {
        return await this.sendBackendRequest("ConnectionStatus", "POST", {});
    }

    protected getResourceName(): string {
        return "HypervisorConnection";
    }
}

const hypervisorConnectionClient = new HypervisorConnectionResourceClient();

export { hypervisorConnectionClient};