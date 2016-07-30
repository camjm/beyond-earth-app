using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class BuildingByIdProcessor : IBuildingByIdProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IBuildingLinkService _buildingLinkService;
        private readonly IBuildingByIdQueryProcessor _buildingByIdQueryProcessor;

        public BuildingByIdProcessor(
            IAutoMapper autoMapper, 
            IBuildingLinkService buildingLinkService,
            IBuildingByIdQueryProcessor buildingByIdQueryProcessor)
        {
            _autoMapper = autoMapper;
            _buildingLinkService = buildingLinkService;
            _buildingByIdQueryProcessor = buildingByIdQueryProcessor;
        }

        public Building GetBuilding(long buildingId)
        {
            var buildingEntity = _buildingByIdQueryProcessor.GetBuilding(buildingId);

            if (buildingEntity == null)
            {
                throw new RootObjectNotFoundException("Building not found");
            }

            var building = _autoMapper.Map<Building>(buildingEntity);

            _buildingLinkService.AddLinks(building);

            return building;
        }
    }
}