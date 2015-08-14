using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using log4net;

namespace BeyondEarthApp.Web.Common.Security
{
    /// <summary>
    /// Demonstrates the ability to do something useful for security: non-invasive auditing of user actions.
    /// Can be applied to an action method, controller, or globally.
    /// </summary>
    public class UserAuditAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public UserAuditAttribute() : this(WebContainerManager.Get<ILogManager>(), WebContainerManager.Get<IUserSession>())
        {
            
        }

        public UserAuditAttribute(ILogManager logManager, IUserSession userSession)
        {
            _log = logManager.GetLog(typeof(UserAuditAttribute));
            _userSession = userSession;
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            _log.Debug("Starting execution...");

            // Have to capture user name here, because HttpContext.Current.User is not available in the AuditCurrentUser method
            var username = _userSession.Username;

            return Task.Run(() => AuditCurrentUser(username));
        }

        public void AuditCurrentUser(string username)
        {
            // Simulate long auditing process e.g. writing auditing information to the database, not a log file
            _log.InfoFormat("Action being executed by user={0}", username);
            Thread.Sleep(3000);
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            _log.InfoFormat("Action executed by user={0}", _userSession.Username);
        }
    }
}
