using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BeyondEarthApp.Common;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using BeyondEarthApp.Web.Common.Validation;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("buildings")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    [EnableCors("http://127.0.0.1:8080", "*", "*")]
    public class BuildingsController : ApiController
    {
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly IUpdateBuildingMaintenanceProcessor _updateBuildingMaintenanceProcessor;
        private readonly IAddBuildingMaintenanceProcessor _addBuildingMaintenanceProcessor;
        private readonly IDeleteBuildingQueryProcessor _deleteBuildingQueryProcessor;
        private readonly IBuildingByIdProcessor _buildingByIdProcessor;
        private readonly IAllBuildingsProcessor _allBuildingsProcessor;

        public BuildingsController(
            IPagedDataRequestFactory pagedDataRequestFactory,
            IUpdateBuildingMaintenanceProcessor updateBuildingMaintenanceProcessor,
            IAddBuildingMaintenanceProcessor addBuildingMaintenanceProcessor,
            IDeleteBuildingQueryProcessor deleteBuildingQueryProcessor,
            IBuildingByIdProcessor buildingByIdProcessor,
            IAllBuildingsProcessor allBuildingsProcessor)
        {
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _updateBuildingMaintenanceProcessor = updateBuildingMaintenanceProcessor;
            _addBuildingMaintenanceProcessor = addBuildingMaintenanceProcessor;
            _deleteBuildingQueryProcessor = deleteBuildingQueryProcessor;
            _buildingByIdProcessor = buildingByIdProcessor;
            _allBuildingsProcessor = allBuildingsProcessor;
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetBuildingRoute")]
        public Building GetBuilding(long id)
        {
            var building = _buildingByIdProcessor.GetBuilding(id);
            return building;
        }

        [HttpGet]
        [Route("", Name = "GetBuildingsRoute")]
        public PagedDataInquiryResponse<BuildingPrecis> GetBuildings(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);
            var buildings = _allBuildingsProcessor.GetBuildings(request);
            return buildings;
        }

        [Route("", Name = "AddBuildingRoute")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddBuilding(HttpRequestMessage requestMessage, Building newBuilding)
        {
            // Delegate all work to maintenance processor
            var building = _addBuildingMaintenanceProcessor.AddBuilding(newBuilding);
            var result = new CreatedActionResult<Building>(building, requestMessage);
            return result;
        }

        [HttpPut]
        [HttpPatch]
        //[ValidateGameUpdateRequest] TODO: generice ValidateUpdateRequest attribute
        [Route("{id:long}", Name = "UpdateBuildingRoute")]
        public Building UpdateBuilding(long id, [FromBody] object updatedBuilding)
        {
            // object type makes partial updates possible by allowing a sparse representation of Building. 
            // If ASP.NET Web API model binding was used, we wouldn't know what the caller wanted to partially update.
            var building = _updateBuildingMaintenanceProcessor.UpdateBuilding(id, updatedBuilding);
            return building;
        }

        [HttpDelete]
        [Route("{id:long}", Name = "DeleteBuildingRoute")]
        public IHttpActionResult DeleteBuilding(long id)
        {
            _deleteBuildingQueryProcessor.DeleteBuilding(id);
            return Ok();
        }
    }
}
