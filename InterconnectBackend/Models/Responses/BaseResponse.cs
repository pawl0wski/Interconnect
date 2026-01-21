namespace Models.Responses
{
    /// <summary>
    /// Base class for API responses.
    /// </summary>
    /// <typeparam name="TResponse">Type of response data.</typeparam>
    /// <typeparam name="TSelf">Type of the inheriting class.</typeparam>
    public abstract class BaseResponse<TResponse, TSelf> where TSelf : BaseResponse<TResponse, TSelf>, new()
    {
        /// <summary>
        /// Indicates whether the operation was successful.
        /// </summary>
        public bool Success { get; set; }
        
        /// <summary>
        /// Error message, if any error occurred.
        /// </summary>
        public string? ErrorMessage { get; set; }
        
        /// <summary>
        /// Response data.
        /// </summary>
        public TResponse? Data { get; set; }

        /// <summary>
        /// Creates a successful response with data.
        /// </summary>
        /// <param name="data">Data to return.</param>
        /// <returns>Successful response.</returns>
        public static TSelf WithSuccess(TResponse data)
        {
            return new TSelf
            {
                Success = true,
                Data = data
            };
        }
        
        /// <summary>
        /// Creates an empty successful response.
        /// </summary>
        /// <returns>Empty successful response.</returns>
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
