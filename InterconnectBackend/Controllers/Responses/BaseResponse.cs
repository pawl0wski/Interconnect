namespace Models.Responses
{
    public class BaseResponse<TResponse>
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public TResponse? Data { get; set; }

        public static BaseResponse<TResponse> WithSuccess(TResponse data)
        {
            return new BaseResponse<TResponse>
            {
                Success = true,
                Data = data
            };
        }
        public static BaseResponse<object> WithEmptySuccess()
        {
            return new BaseResponse<object>
            {
                Success = true,
                Data = new object()
            };
        }
    }
}
