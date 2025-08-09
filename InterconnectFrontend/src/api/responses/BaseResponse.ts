interface BaseResponse<TData> {
    success: boolean;
    errorMessage: string | null;
    data: TData;
}

export default BaseResponse;