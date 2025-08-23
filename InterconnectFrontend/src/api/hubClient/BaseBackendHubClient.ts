import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { getConfiguration } from "../../configuration.ts";

abstract class BaseBackendHubClient {
    protected connection?: HubConnection;

    async connect() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.prepareBackendUrl())
            .withAutomaticReconnect()
            .build();
        await this.connection.start();
    }

    protected startListeningForMessages<T>(methodName: string, onMessage: (response: T) => void) {
        this.connection?.on(methodName, (message: string) => {
            onMessage(JSON.parse(message));
        });
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
        if (this.connection) {
            return;
        }

        await this.connect();
    }

    private prepareBackendUrl(): string {
        const config = getConfiguration();
        const hubName = this.getHubName();
        return `${config.backendUrl}${hubName}`;
    }


    protected abstract getHubName(): string;
}

export default BaseBackendHubClient;