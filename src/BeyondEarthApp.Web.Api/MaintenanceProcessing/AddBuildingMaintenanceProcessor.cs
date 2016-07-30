using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class AddBuildingMaintenanceProcessor : IAddBuildingMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IBuildingLinkService _buildingLinkService;
        private readonly IAddBuildingQueryProcessor _queryProcessor;

        public AddBuildingMaintenanceProcessor(
            IAutoMapper autoMapper,
            IBuildingLinkService buildingLinkService,
            IAddBuildingQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _buildingLinkService = buildingLinkService;
        }

        public Building AddBuilding(Building newBuilding)
        {
            // Map service model to entity model
            var buildingEntity = _autoMapper.Map<Data.Entities.Building>(newBuilding);

            // Persist entity model
            _queryProcessor.AddBuilding(buildingEntity);

            // Map new entity model back to full service model
            var building = _autoMapper.Map<Building>(buildingEntity);

            // Add links to service model
            _buildingLinkService.AddLinks(building);

            return building;
        }
    }
}