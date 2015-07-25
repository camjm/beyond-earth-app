using System.Net.Http;
using System.Web.Http;
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
        public Technology AddTechnology(HttpRequestMessage requestMessage, NewTechnology newTechnology)
        {
            // Delegate all work to maintenance processor
            var technology = _addTechnologyMaintenanceProcessor.AddTechnology(newTechnology);
            return technology;
        }
    }
}
