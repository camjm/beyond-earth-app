using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeyondEarthApp.Web.Common.Validation
{
    /// <summary>
    /// General validation action filter that can decorate any controller action method. 
    /// Validation using standard .NET data annotations decorating the target classes.
    /// </summary>
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // Leverage ASP.NET Web API model-binding process, which performs the validation for us, based on data annotations
            if (!actionContext.ModelState.IsValid)
            {
                // prevents processing from reaching the controller's action method 
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    actionContext.ModelState);
            }
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }
    }
}
