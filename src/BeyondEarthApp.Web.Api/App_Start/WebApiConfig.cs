using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Routing;
using System.Web.Http.Tracing;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.ErrorHandling;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("apiVersionConstraint", typeof(ApiVersionConstraint));

            config.MapHttpAttributeRoutes(constraintResolver);

            // Register custom implementations with the framework
            config.Services.Replace(
                typeof(IHttpControllerSelector), 
                new NamespaceHttpControllerSelector(config));
            config.Services.Replace(
                typeof(ITraceWriter), 
                new SimpleTraceWriter(WebContainerManager.Get<ILogManager>()));
            config.Services.Replace(
                typeof(IExceptionLogger), 
                new SimpleExceptionLogger(WebContainerManager.Get<ILogManager>()));
            config.Services.Replace(
                typeof(IExceptionHandler), 
                new GlobalExceptionHandler());
        }
    }
}
