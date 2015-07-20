using System.Web.Http.ExceptionHandling;
using BeyondEarthApp.Common.Logging;
using log4net;

namespace BeyondEarthApp.Web.Common.ErrorHandling
{
    /// <summary>
    /// Simply logs exceptions.
    /// </summary>
    public class SimpleExceptionLogger : ExceptionLogger
    {
        private readonly ILog _log;

        public SimpleExceptionLogger(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(SimpleExceptionLogger));
        }

        public override void Log(ExceptionLoggerContext context)
        {
            // have access to the entire exception context, including the HttpRequestMessage, not just the exception
            _log.Error("Unhandled Exception", context.Exception);
        }
    }
}
