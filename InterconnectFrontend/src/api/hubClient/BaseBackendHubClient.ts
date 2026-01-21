import { HubConnection, HubConnectionBuilder } from "@microsoft/signalr";
import { getConfiguration } from "../../configuration.ts";

/**
 * Abstract base class for SignalR hub client implementations.
 * Manages WebSocket connections to backend hubs and provides methods for sending/receiving real-time messages.
 */
abstract class BaseBackendHubClient {
    /** The SignalR hub connection instance */
    protected connection?: HubConnection;

    /**
     * Establishes a connection to the backend hub.
     * Includes automatic reconnection capability.
     * @returns {Promise<void>}
     */
    async connect() {
        this.connection = new HubConnectionBuilder()
            .withUrl(this.prepareBackendUrl())
            .withAutomaticReconnect()
            .build();
        await this.connection.start();
    }

    /**
     * Registers a listener for incoming hub messages.
     * Messages are automatically parsed from JSON.
     * @template T The type of message data
     * @param {string} methodName The hub method name to listen for
     * @param {Function} onMessage Callback function invoked when a message is received
     */
    protected startListeningForMessages<T>(
        methodName: string,
        onMessage: (response: T) => void,
    ) {
        this.connection?.on(methodName, (message: string) => {
            onMessage(JSON.parse(message));
        });
    }

    /**
     * Sends a request to the hub and receives a response.
     * @template TRequest The type of request object
     * @template TResponse The type of response object
     * @param {string} methodName The hub method name to invoke
     * @param {TRequest} request Optional request payload
     * @returns {Promise<TResponse>} The response from the hub method
     */
    protected async sendHubRequest<TRequest, TResponse>(
        methodName: string,
        request?: TRequest,
    ): Promise<TResponse> {
        await this.reconnectIfThereIsNoConnectionEstablished();

        let response;
        if (request == null) {
            response = await this.connection?.invoke(methodName);
        } else {
            response = await this.connection?.invoke(methodName, request);
        }

        return response;
    }

    /**
     * Reconnects to the hub if the connection has been lost.
     * @returns {Promise<void>}
     */
    protected async reconnectIfThereIsNoConnectionEstablished() {
        if (this.connection) {
            return;
        }

        await this.connect();
    }

    /**
     * Returns the hub name for this client (e.g., "ConnectionStatusHub").
     * Subclasses must implement this method.
     * @returns {string} The hub name
     */
    protected abstract getHubName(): string;

    /**
     * Constructs the full URL for the hub connection.
     * Format: {backendUrl}{hubName}
     * @returns {string} The full hub URL
     */
    private prepareBackendUrl(): string {
        const config = getConfiguration();
        const hubName = this.getHubName();
        return `${config.backendUrl}${hubName}`;
    }
}

export default BaseBackendHubClient;
