using Microsoft.AspNetCore.Mvc;
using Models.Responses;

namespace TestUtils
{
    public static class ResponseExtractor
    {
        public static TResponse? ExtractControllerResponse<TResponse, TData>(
            ActionResult<TResponse> controllerResponse)
            where TResponse : BaseResponse<TData, TResponse>, new()
        {
            var result = controllerResponse.Result as ObjectResult;
            return result?.Value as TResponse;
        }
    }
}
