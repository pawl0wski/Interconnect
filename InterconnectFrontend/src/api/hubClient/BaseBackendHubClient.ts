import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { getConfiguration } from "../../configuration.ts";

abstract class BaseBackendHubClient {
    protected connection?: HubConnection;

    public async connect() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.prepareBackendUrl())
            .build();
        await this.connection.start();
    }

    protected async sendHubRequest<TRequest, TResponse>(methodName: string, request?: TRequest): Promise<TResponse> {
        await this.reconnectIfThereIsNoConnectionEstablished();

        let response;
        if (request == null) {
            response = await this.connection?.invoke(methodName);
        } else {
            response = await this.connection?.invoke(methodName, request);
        }

        return response;
    }


    protected async reconnectIfThereIsNoConnectionEstablished() {
        if (this.isConnectionEstablished()) {
            return;
        }

        await this.connect();
    }

    protected isConnectionEstablished() {
        return Boolean(this.connection?.connectionId);
    }

    private prepareBackendUrl(): string {
        const config = getConfiguration();
        const hubName = this.getHubName();
        return `${config.backendUrl}${hubName}`;
    }

    protected abstract getHubName(): string;
}

export default BaseBackendHubClient;