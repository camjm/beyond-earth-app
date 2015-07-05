using System.Web.Http;

namespace BeyondEarthApp.Web.Common.Routing
{
    /// <summary>
    /// This class is just syntactic sugar and encapsulates the "api/v1" part of the route template, instead of using the RoutePrefixAttribute class.
    /// </summary>
    public class ApiVersion1RoutePrefixAttribute : RoutePrefixAttribute
    {
        private const string RouteBase = "api/{apiVersion:apiVersionConstraint(v1)}";

        private const string PrefixRouteBase = RouteBase + "/";

        public ApiVersion1RoutePrefixAttribute(string routePrefix) 
            : base(string.IsNullOrWhiteSpace(routePrefix) ? RouteBase : PrefixRouteBase + routePrefix)
        {
            
        }
    }
}