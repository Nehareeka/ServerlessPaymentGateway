using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.Features;
using System.Net;

namespace PaymentGateway.API.Exceptions
{
    public static class ExceptionMiddlewareExtensions
    {
        //Custom Exception Handler
        public static void ConfigureCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandler>();
        }
    }
}
