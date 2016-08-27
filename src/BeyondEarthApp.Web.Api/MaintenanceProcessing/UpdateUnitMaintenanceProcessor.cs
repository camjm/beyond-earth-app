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
    public class UpdateUnitMaintenanceProcessor : IUpdateUnitMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUnitLinkService _unitLinkService;
        private readonly IUpdateUnitQueryProcessor _queryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateUnitMaintenanceProcessor(
            IAutoMapper autoMapper,
            IUnitLinkService unitLinkService,
            IUpdateUnitQueryProcessor queryProcessor,
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _unitLinkService = unitLinkService;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        /// <summary>
        /// Uses JObject to parse the unit fragment into an actual Unit instance, persists the updates, and maps the returned entity to the service model.
        /// </summary>
        public Unit UpdateUnit(long unitId, object unitFragment)
        {
            var unitFragmentAsJObject = (JObject)unitFragment;

            // parse unit fragment into actual Unit instance
            var unitContainingUpdateData = unitFragmentAsJObject.ToObject<Unit>();
            var entityContainingUpdateData = _autoMapper.Map<Data.Entities.Unit>(unitContainingUpdateData);

            // compute the Property Value Map
            var updatedPropertyValueMap = GetPropertyValueMap(unitFragmentAsJObject, entityContainingUpdateData);

            // update the Unit and persist the changes
            var updatedUnitEntity = _queryProcessor.GetUpdatedUnit(unitId, updatedPropertyValueMap);

            // map Unit entity to the service model object
            var unit = _autoMapper.Map<Unit>(updatedUnitEntity);

            _unitLinkService.AddLinks(unit);

            return unit;
        }

        /// <summary>
        /// Creates the PropertyValueMap instance and populates it with the new values.
        /// </summary>
        public virtual PropertyValueMapType GetPropertyValueMap(JObject unitFragment, Data.Entities.Unit entityContainingUpdateData)
        {
            // detirmine the names of the properties that need to be updated
            var namesOfModifiedProperties = _updateablePropertyDetector
                .GetNamesOfPropertiesToUpdate<Unit>(unitFragment)
                .ToList();

            var propertyInfos = typeof(Data.Entities.Unit).GetProperties();

            var updatedPropertyValueMap = new PropertyValueMapType();

            // for each of these properties get the corresponding value and add it to the map
            foreach (var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(entityContainingUpdateData);
                updatedPropertyValueMap.Add(propertyName, propertyValue);
            }

            return updatedPropertyValueMap;
        }
    }
}