using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using Newtonsoft.Json.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class UpdateBuildingMaintenanceProcessor : IUpdateBuildingMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IBuildingLinkService _buildingLinkService;
        private readonly IUpdateBuildingQueryProcessor _queryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateBuildingMaintenanceProcessor(
            IAutoMapper autoMapper,
            IBuildingLinkService buildingLinkService,
            IUpdateBuildingQueryProcessor queryProcessor,
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _buildingLinkService = buildingLinkService;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        /// <summary>
        /// Uses JObject to parse the building fragment into an actual Building instance, persists the updates, and maps the returned entity to the service model.
        /// </summary>
        public Building UpdateBuilding(long buildingId, object buildingFragment)
        {
            var buildingFragmentAsJObject = (JObject)buildingFragment;

            // parse building fragment into actual Building instance
            var buildingContainingUpdateData = buildingFragmentAsJObject.ToObject<Building>();

            // compute the Property Value Map
            var updatedPropertyValueMap = GetPropertyValueMap(buildingFragmentAsJObject, buildingContainingUpdateData);

            // update the Building and persist the changes
            var updatedBuildingEntity = _queryProcessor.GetUpdatedBuilding(buildingId, updatedPropertyValueMap);

            // map Building entity to the service model object
            var building = _autoMapper.Map<Building>(updatedBuildingEntity);

            _buildingLinkService.AddLinks(building);

            return building;
        }

        /// <summary>
        /// Creates the PropertyValueMap instance and populates it with the new values.
        /// </summary>
        public virtual PropertyValueMapType GetPropertyValueMap(JObject buildingFragment, Building buildingContainingUpdateData)
        {
            // detirmine the names of the properties that need to be updated
            var namesOfModifiedProperties = _updateablePropertyDetector
                .GetNamesOfPropertiesToUpdate<Building>(buildingFragment)
                .ToList();

            var propertyInfos = typeof(Building).GetProperties();

            var updatedPropertyValueMap = new PropertyValueMapType();

            // for each of these properties get the corresponding value and add it to the map
            foreach (var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(buildingContainingUpdateData);
                updatedPropertyValueMap.Add(propertyName, propertyValue);
            }

            return updatedPropertyValueMap;
        }
    }
}