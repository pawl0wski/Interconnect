using Interconnect.Middlewares;
using Microsoft.AspNetCore.Http;
using Models.Responses;
using System.Text.Json;

namespace InterconnectTests
{
    public class ExceptionHandlerMiddlewareTests
    {
        [Test]
        public async Task InvokeAsync_NoException_CallsNextMiddleware()
        {
            var context = new DefaultHttpContext();
            var wasCalled = false;

            RequestDelegate next = (ctx) =>
            {
                wasCalled = true;
                return Task.CompletedTask;
            };

            var middleware = new ExceptionHandlerMiddleware(next);

            await middleware.InvokeAsync(context);

            Assert.IsTrue(wasCalled);
            Assert.That(context.Response.StatusCode, Is.EqualTo(200));
        }

        [Test]
        public async Task InvokeAsync_WhenExceptionThrown_ReturnsErrorResponse()
        {
            var context = new DefaultHttpContext();
            var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            var exceptionMessage = "Mock exception";

            RequestDelegate next = (ctx) =>
            {
                throw new InvalidOperationException(exceptionMessage);
            };

            var middleware = new ExceptionHandlerMiddleware(next);

            await middleware.InvokeAsync(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseText = await new StreamReader(context.Response.Body).ReadToEndAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var response = JsonSerializer.Deserialize<BaseResponse<object>>(responseText, options);

            Assert.That(context.Response.StatusCode, Is.EqualTo(500));
            Assert.IsNotNull(response);
            Assert.IsFalse(response!.Success);
            Assert.That(response.ErrorMessage, Is.EqualTo(exceptionMessage));
        }
    }
}