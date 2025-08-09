import BaseResponse from "./responses/BaseResponse.ts";
import BaseRequest from "./requests/BaseRequest.ts";
import { getConfiguration } from "../configuration.ts";

abstract class BaseBackendResourceClient {
    protected async sendBackendRequest<
        TRequest extends BaseRequest,
        TResponse extends BaseResponse<unknown>
    >(
        url: string,
        method: string,
        request: TRequest | null
    ): Promise<TResponse> {
        let fetchParams: RequestInit = { method };
        if (method !== "GET") {
            fetchParams = {
                ...fetchParams,
                body: JSON.stringify(request),
                headers: {
                    "Content-Type": "application/json"
                }
            };
        }

        const fetchResponse = await fetch(this.prepareBackendUrl(url), fetchParams);

        const response = await fetchResponse.json() as BaseResponse<object>;
        if (!response.success) {
            throw Error(response.errorMessage ?? undefined);
        }

        return response as TResponse;
    }

    protected prepareBackendUrl(methodName: string): string {
        const config = getConfiguration();

        const backendUrl = new URL(config.backendUrl);
        backendUrl.pathname = `/${this.getResourceName()}/${methodName}`;

        return backendUrl.toString();
    }

    protected abstract getResourceName(): string;
}

export default BaseBackendResourceClient;