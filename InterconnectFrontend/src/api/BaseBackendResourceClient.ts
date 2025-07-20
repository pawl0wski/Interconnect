import BaseResponse from "./responses/BaseResponse.ts";
import BaseRequest from "./requests/BaseRequest.ts";
import { getConfiguration } from "../Configuration.ts";

abstract class BaseBackendResourceClient {
    protected async sendBackendRequest<
        TRequest extends BaseRequest,
        TResponse extends BaseResponse<unknown>
    >(
        url: string,
        method: string,
        request: TRequest
    ): Promise<TResponse> {
        const fetchResponse = await fetch(this.prepareBackendUrl(url), { method, body: JSON.stringify(request) });

        const response = await fetchResponse.json() as BaseResponse<object>;
        if (!response.success) {
            throw Error(response.message);
        }

        return response as TResponse;
    }

    protected prepareBackendUrl(methodName: string): string {
        const config = getConfiguration();

        const backendUrl = new URL(config.BackendUrl);
        backendUrl.pathname = `/${this.getResourceName()}/${methodName}`;

        return backendUrl.toString();
    }

    protected abstract getResourceName(): string;
}

export default BaseBackendResourceClient;