import BaseBackendHubClient from "./BaseBackendHubClient.ts";
import StringResponse from "../responses/StringResponse.ts";

class ConnectionStatusHubClient extends BaseBackendHubClient {
    public async ping(): Promise<StringResponse> {
        return await this.sendHubRequest("Ping");
    }

    protected getHubName(): string {
        return "ConnectionStatusHub";
    }
}

const connectionStatusHubClient = new ConnectionStatusHubClient();

export { connectionStatusHubClient };