using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeyondEarthApp.Web.Common
{
    /// <summary>
    /// Handles disposal of ISession. Actions can get an open ISession with dependency injection.
    /// Wraps all request database operations within a single transaction by default.
    /// </summary>
    public class UnitOfWorkActionFilterAttribute : ActionFilterAttribute
    {
        // Attributes cannot use constructor injection because the runtime only ever creates on instance of the Attribute
        public virtual IActionTransactionHelper ActionTransactionHelper
        {
            get { return WebContainerManager.Get<IActionTransactionHelper>(); }
        }

        // Prevent attribute being executed more than once on the same request
        public override bool AllowMultiple
        {
            get { return false; }
        }

        /// <summary>
        /// Called by the framework before the controller action method is called.
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ActionTransactionHelper.BeginTransaction();
        }

        /// <summary>
        /// Called by the framework after the controller method is called.
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ActionTransactionHelper.EndTransaction(actionExecutedContext);
            ActionTransactionHelper.CloseSession();
        }
    }
}
