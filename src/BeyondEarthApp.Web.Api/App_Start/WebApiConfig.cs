using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using Newtonsoft.Json.Serialization;

namespace BeyondEarthApp.Web.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var constraintResolver = new DefaultInlineConstraintResolver();
            constraintResolver.ConstraintMap.Add("apiVersionConstraint", typeof(ApiVersionConstraint));

            config.MapHttpAttributeRoutes();

            config.Services.Replace(typeof(IHttpControllerSelector), new NamespaceHttpControllerSelector(config));
            //config.Services.Replace(typeof(ITraceWriter), null);
            //config.Services.Replace(typeof(IExceptionLogger), null);
            //config.Services.Replace(typeof(IExceptionHandler), null);
        }
    }
}
