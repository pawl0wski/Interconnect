/**
 * Base response type for all API responses from the backend.
 * @template TData The type of data contained in the response
 */
interface BaseResponse<TData> {
    /** Whether the request was successful */
    success: boolean;
    /** Error message if the request failed, null if successful */
    errorMessage: string | null;
    /** Response data of generic type */
    data: TData;
}

export default BaseResponse;
