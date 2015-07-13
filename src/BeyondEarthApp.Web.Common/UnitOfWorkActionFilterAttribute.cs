using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeyondEarthApp.Web.Common
{
    /// <summary>
    /// Handles disposal of ISession. Actions can get an open ISession with dependency injection.
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

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            ActionTransactionHelper.BeginTransaction();
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            ActionTransactionHelper.EndTransaction(actionExecutedContext);
            ActionTransactionHelper.CloseSession();
        }
    }
}
