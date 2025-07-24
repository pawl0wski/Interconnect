import BaseBackendResourceClient from "./BaseBackendResourceClient.ts";
import BaseResponse from "./responses/BaseResponse.ts";
import ConnectionInfo from "../models/ConnectionInfo.ts";

class HypervisorConnectionResourceClient extends BaseBackendResourceClient {
    public async ping(): Promise<BaseResponse<string>> {
        return await this.sendBackendRequest("Ping", "GET", {});
    }

    public async connectionInfo(): Promise<BaseResponse<ConnectionInfo>> {
        return await this.sendBackendRequest("ConnectionInfo", "GET", {});
    }

    protected getResourceName(): string {
        return "HypervisorConnection";
    }
}

const hypervisorConnectionClient = new HypervisorConnectionResourceClient();

export { hypervisorConnectionClient };