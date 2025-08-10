namespace Models.Responses
{
    public abstract class BaseResponse<TResponse, TSelf> where TSelf : BaseResponse<TResponse, TSelf>, new()
    {
        public bool Success { get; set; }
        public string? ErrorMessage { get; set; }
        public TResponse? Data { get; set; }

        public static TSelf WithSuccess(TResponse data)
        {
            return new TSelf
            {
                Success = true,
                Data = data
            };
        }
        public static TSelf WithEmptySuccess()
        {
            return new TSelf
            {
                Success = true,
                Data = default
            };
        }
    }
}
