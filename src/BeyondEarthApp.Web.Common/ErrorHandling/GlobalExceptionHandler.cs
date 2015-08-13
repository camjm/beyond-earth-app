using System.Net;
using System.Web;
using System.Web.Http.ExceptionHandling;
using BeyondEarthApp.Common;
using BeyondEarthApp.Data.Exceptions;

namespace BeyondEarthApp.Web.Common.ErrorHandling
{
    /// <summary>
    /// Replaces the default ASP.NET Web API Exception Handler.
    /// Allows global custom behaviour of unhandled exceptions.
    /// Customize the HTTP response that is sent when an unhandled exception occurs in the application.
    /// Globally handle exceptions by creating appropriate HTTP error responses.
    /// </summary>
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var exception = context.Exception;

            var httpException = exception as HttpException;
            if (httpException != null)
            {
                context.Result = new SimpleErrorResult(
                    context.Request, 
                    (HttpStatusCode) httpException.GetHttpCode(), 
                    httpException.Message);
                return;
            }

            if (exception is RootObjectNotFoundException)
            {
                context.Result = new SimpleErrorResult(
                    context.Request, 
                    HttpStatusCode.NotFound, 
                    exception.Message);
                return;
            }

            if (exception is ChildObjectNotFoundException)
            {
                context.Result = new SimpleErrorResult(
                    context.Request,
                    HttpStatusCode.Conflict,
                    exception.Message);
                return;
            }

            if (exception is BusinessRuleViolationException)
            {
                context.Result = new SimpleErrorResult(
                    context.Request,
                    HttpStatusCode.PaymentRequired,
                    exception.Message);
                return;
            }

            context.Result = new SimpleErrorResult(
                context.Request,
                HttpStatusCode.InternalServerError,
                exception.Message);
        }
    }
}
