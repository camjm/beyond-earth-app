using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("technologies")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class TechnologiesController : ApiController
    {
        private readonly IAddTechnologyMaintenanceProcessor _addTechnologyMaintenanceProcessor;
        private readonly ITechnologyByIdProcessor _technologyByIdProcessor;

        public TechnologiesController(
            IAddTechnologyMaintenanceProcessor addTechnologyMaintenanceProcessor,
            ITechnologyByIdProcessor technologyByIdProcessor)
        {
            _addTechnologyMaintenanceProcessor = addTechnologyMaintenanceProcessor;
            _technologyByIdProcessor = technologyByIdProcessor;
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

        [HttpGet]
        [Route("{id:long}", Name = "GetTechnologyRoute")]
        public Technology GetTechnology(long id)
        {
            var technology = _technologyByIdProcessor.GetTechnology(id);
            return technology;
        }
    }
}
