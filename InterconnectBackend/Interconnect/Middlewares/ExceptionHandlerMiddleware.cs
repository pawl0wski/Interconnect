using Models.Responses;

namespace Interconnect.Middlewares
{
    /// <summary>
    /// Middleware for global exception handling in the application.
    /// </summary>
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Processes HTTP request and catches exceptions.
        /// </summary>
        /// <param name="context">HTTP context.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";

                var response = new ExceptionResponse
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    Data = new object()
                };

                await context.Response.WriteAsJsonAsync(response);
            }
        }
    }

    /// <summary>
    /// Extensions for registering exception handling middleware.
    /// </summary>
    public static class ExceptionHandlerMiddlewareExtensions
    {
        /// <summary>
        /// Adds exception handling middleware to the application pipeline.
        /// </summary>
        /// <param name="builder">Application builder.</param>
        /// <returns>Application builder with added middleware.</returns>
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}

