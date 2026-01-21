import BaseResponse from "../responses/BaseResponse.ts";
import BaseRequest from "../requests/BaseRequest.ts";
import { getConfiguration } from "../../configuration.ts";

/**
 * Abstract base class for all backend resource client implementations.
 * Provides common methods for sending HTTP requests to the backend API and handling responses.
 */
abstract class BaseBackendResourceClient {
    /**
     * Sends an HTTP request to the backend and handles the response.
     * @template TRequest The type of request object
     * @template TResponse The type of response object
     * @param {string} url The endpoint path (appended to resource name)
     * @param {string} method The HTTP method (GET, POST, PUT, etc.)
     * @param {TRequest | null} request The request payload (null for GET requests)
     * @returns {Promise<TResponse>} The typed response from the backend
     * @throws {Error} If the backend returns success: false
     */
    protected async sendBackendRequest<
        TRequest extends BaseRequest,
        TResponse extends BaseResponse<unknown>,
    >(
        url: string,
        method: string,
        request: TRequest | null,
    ): Promise<TResponse> {
        let fetchParams: RequestInit = { method };
        if (method !== "GET") {
            fetchParams = {
                ...fetchParams,
                body: JSON.stringify(request),
                headers: {
                    "Content-Type": "application/json",
                },
            };
        }

        const fetchResponse = await fetch(
            this.prepareBackendUrl(url),
            fetchParams,
        );

        const response = (await fetchResponse.json()) as BaseResponse<object>;
        if (!response.success) {
            throw Error(response.errorMessage ?? undefined);
        }

        return response as TResponse;
    }

    /**
     * Constructs the full URL for the backend request.
     * Format: {backendUrl}/{resourceName}/{methodName}
     * @param {string} methodName The API method name
     * @returns {string} The full URL for the request
     */
    protected prepareBackendUrl(methodName: string): string {
        const config = getConfiguration();

        const backendUrl = new URL(config.backendUrl);
        backendUrl.pathname = `/${this.getResourceName()}/${methodName}`;

        return backendUrl.toString();
    }

    /**
     * Returns the resource name for this client (e.g., "Entity", "VirtualMachine").
     * Subclasses must implement this method to define their resource path.
     * @returns {string} The resource name
     */
    protected abstract getResourceName(): string;
}

export default BaseBackendResourceClient;
