import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import StringResponse from "../responses/StringResponse.ts";

/**
 * Hub client for connection status monitoring using SignalR.
 */
class ConnectionStatusHubClient extends BaseBackendHubClient {
    /**
     * Sends a ping request to the backend to verify the connection status.
     * @returns {Promise<StringResponse>} Response from the backend ping
     */
    public async ping(): Promise<StringResponse> {
        return await this.sendHubRequest("Ping");
    }

    protected getHubName(): string {
        return "ConnectionStatusHub";
    }
}

const connectionStatusHubClient = new ConnectionStatusHubClient();

export { connectionStatusHubClient };
