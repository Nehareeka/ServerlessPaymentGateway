using System.Net;

namespace PaymentGateway.API.Exceptions
{
    public class CustomExceptionHandler
    {
        private readonly RequestDelegate _requestDelegate;

        public CustomExceptionHandler(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (CardDetailsException e)
            {
                await HandleExceptionAsync(httpContext, (int)HttpStatusCode.UnprocessableEntity, $"{e.PaymentId}: {e.Message}");
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, (int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private Task HandleExceptionAsync(HttpContext httpContext, int statusCode, string message)
        {
            httpContext.Response.ContentType = "application/json";

            var errorString = new ErrorResponseData()
            {
                StatusCode = statusCode,
                Message = message,
                Path = httpContext.Request.Path
            }.ToString();

            httpContext.Response.StatusCode = statusCode;
            return httpContext.Response.WriteAsync(errorString);
        }
    }
}
