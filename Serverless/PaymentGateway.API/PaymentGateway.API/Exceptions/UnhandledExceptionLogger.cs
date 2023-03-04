using System.Diagnostics;
using System.Web.Http.ExceptionHandling;

namespace PaymentGateway.API.Exceptions
{
    public class UnhandledExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var log = context.Exception.Message;
            // To Do: have a database to store these exceptions
            Debug.WriteLine(log);
        }
    }
}
