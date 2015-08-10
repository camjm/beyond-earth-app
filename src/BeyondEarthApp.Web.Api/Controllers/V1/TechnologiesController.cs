using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("technologies")]
    [UnitOfWorkActionFilter]
    public class TechnologiesController : ApiController
    {
        private readonly IAddTechnologyMaintenanceProcessor _addTechnologyMaintenanceProcessor;

        public TechnologiesController(IAddTechnologyMaintenanceProcessor addTechnologyMaintenanceProcessor)
        {
            _addTechnologyMaintenanceProcessor = addTechnologyMaintenanceProcessor;
        }

        [Route("", Name = "AddTechnologyRoute")]
        [HttpPost]
        [Authorize(Roles = Constants.RoleNames.Admin)]  // Security Principal on the current HttpContext must have Admin role. If unauthorized, returns a 401 HTTP status code
        public IHttpActionResult AddTechnology(HttpRequestMessage requestMessage, NewTechnology newTechnology)
        {
            // Delegate all work to maintenance processor
            var technology = _addTechnologyMaintenanceProcessor.AddTechnology(newTechnology);
            var result = new CreatedActionResult<Technology>(technology, requestMessage);
            return result;
        }
    }
}
