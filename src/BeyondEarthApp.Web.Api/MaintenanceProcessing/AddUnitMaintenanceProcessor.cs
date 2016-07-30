using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class AddUnitMaintenanceProcessor : IAddUnitMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUnitLinkService _unitLinkService;
        private readonly IAddUnitQueryProcessor _queryProcessor;

        public AddUnitMaintenanceProcessor(
            IAutoMapper autoMapper,
            IUnitLinkService unitLinkService,
            IAddUnitQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _unitLinkService = unitLinkService;
        }

        public Unit AddUnit(Unit newUnit)
        {
            // Map service model to entity model
            var unitEntity = _autoMapper.Map<Data.Entities.Unit>(newUnit);

            // Persist entity model
            _queryProcessor.AddUnit(unitEntity);

            // Map new entity model back to full service model
            var unit = _autoMapper.Map<Unit>(unitEntity);

            // Add links to service model
            _unitLinkService.AddLinks(unit);

            return unit;
        }
    }
}