using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.Models.InquiryResponses;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [UnitOfWorkActionFilter]
    [ApiVersion1RoutePrefix("statuses")]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class StatusesController : ApiController
    {
        private readonly IAllStatusesProcessor _allStatusesProcessor;

        public StatusesController(IAllStatusesProcessor allStatusesProcessor)
        {
            _allStatusesProcessor = allStatusesProcessor;
        }

        [HttpGet]
        [Route("", Name = "GetStatusesRoute")]
        public StatusesInquiryResponse GetStatuses()
        {
            var inquiryResponse = _allStatusesProcessor.GetStatuses();
            return inquiryResponse;
        }
    }
}
