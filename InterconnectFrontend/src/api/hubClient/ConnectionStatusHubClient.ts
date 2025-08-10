import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import PingResponse from "../responses/PingResponse.ts";

class ConnectionStatusHubClient extends BaseBackendHubClient {
    public async ping(): Promise<PingResponse> {
        return await this.sendHubRequest("Ping");
    }

    protected getHubName(): string {
        return "ConnectionStatusHub";
    }
}

const connectionStatusHubClient = new ConnectionStatusHubClient();

export { connectionStatusHubClient };