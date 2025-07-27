using Microsoft.AspNetCore.Mvc;
using Models.Responses;

namespace TestUtils
{
    public static class ResponseExtractor
    {
        public static BaseResponse<TResponseData>? ExtractControllerResponse<TResponseData>(ActionResult<BaseResponse<TResponseData>> controllerResponse)
        {
            var result = controllerResponse.Result as ObjectResult;

            return result?.Value as BaseResponse<TResponseData>;
        }
    }
}
