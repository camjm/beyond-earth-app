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
    [ApiVersion1RoutePrefix("units")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    [EnableCors("http://127.0.0.1:8080", "*", "*")]
    public class UnitsController : ApiController
    {
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly IUpdateUnitMaintenanceProcessor _updateUnitMaintenanceProcessor;
        private readonly IAddUnitMaintenanceProcessor _addUnitMaintenanceProcessor;
        private readonly IDeleteUnitQueryProcessor _deleteUnitQueryProcessor;
        private readonly IUnitByIdProcessor _unitByIdProcessor;
        private readonly IAllUnitsProcessor _allUnitsProcessor;

        public UnitsController(
            IPagedDataRequestFactory pagedDataRequestFactory,
            IUpdateUnitMaintenanceProcessor updateUnitMaintenanceProcessor,
            IAddUnitMaintenanceProcessor addUnitMaintenanceProcessor,
            IDeleteUnitQueryProcessor deleteUnitQueryProcessor,
            IUnitByIdProcessor unitByIdProcessor,
            IAllUnitsProcessor allUnitsProcessor)
        {
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _updateUnitMaintenanceProcessor = updateUnitMaintenanceProcessor;
            _addUnitMaintenanceProcessor = addUnitMaintenanceProcessor;
            _deleteUnitQueryProcessor = deleteUnitQueryProcessor;
            _unitByIdProcessor = unitByIdProcessor;
            _allUnitsProcessor = allUnitsProcessor;
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetUnitRoute")]
        public Unit GetUnit(long id)
        {
            var unit = _unitByIdProcessor.GetUnit(id);
            return unit;
        }

        [HttpGet]
        [Route("", Name = "GetUnitsRoute")]
        public PagedDataInquiryResponse<UnitPrecis> GetUnits(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);
            var units = _allUnitsProcessor.GetUnits(request);
            return units;
        }

        [Route("", Name = "AddUnitRoute")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddUnit(HttpRequestMessage requestMessage, Unit newUnit)
        {
            // Delegate all work to maintenance processor
            var unit = _addUnitMaintenanceProcessor.AddUnit(newUnit);
            var result = new CreatedActionResult<Unit>(unit, requestMessage);
            return result;
        }

        [HttpPut]
        [HttpPatch]
        //[ValidateGameUpdateRequest] TODO: generice ValidateUpdateRequest attribute
        [Route("{id:long}", Name = "UpdateUnitRoute")]
        public Unit UpdateUnit(long id, [FromBody] object updatedUnit)
        {
            // object type makes partial updates possible by allowing a sparse representation of Unit. 
            // If ASP.NET Web API model binding was used, we wouldn't know what the caller wanted to partially update.
            var unit = _updateUnitMaintenanceProcessor.UpdateUnit(id, updatedUnit);
            return unit;
        }

        [HttpDelete]
        [Route("{id:long}", Name = "DeleteUnitRoute")]
        public IHttpActionResult DeleteUnit(long id)
        {
            _deleteUnitQueryProcessor.DeleteUnit(id);
            return Ok();
        }
    }
}
