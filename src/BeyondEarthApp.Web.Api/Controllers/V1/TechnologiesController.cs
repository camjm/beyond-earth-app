using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("technologies")]
    [UnitOfWorkActionFilter]
    public class TechnologiesController : ApiController
    {
        [Route("", Name = "AddTechnologyRoute")]
        [HttpPost]
        public Technology AddTechnology(HttpRequestMessage requestMessage, NewTechnology newTechnology)
        {
            return new Technology
            {
                Name = "V1: " + newTechnology.Name
            };
        }
    }
}
