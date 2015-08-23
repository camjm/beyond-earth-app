using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using Newtonsoft.Json.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// JSON-specific implementation.
    /// </summary>
    public class UpdateGameMaintenanceProcessor : IUpdateGameMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IUpdateGameQueryProcessor _queryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateGameMaintenanceProcessor(
            IAutoMapper autoMapper, 
            IUpdateGameQueryProcessor queryProcessor, 
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        public Game UpdateGame(long gameId, object gameFragment)
        {
            var gameFragmentAsJObject = (JObject) gameFragment;

            // parse game fragment into actual Game instance
            var gameContainingUpdateData = gameFragmentAsJObject.ToObject<Game>();

            // compute the Property Value Map
            var updatedPropertyValueMap = GetPropertyValueMap(gameFragmentAsJObject, gameContainingUpdateData);

            // update the Game and persist the changes
            var updatedGameEntity = _queryProcessor.GetUpdatedGame(gameId, updatedPropertyValueMap);

            // map Game entity to the service model object
            var game = _autoMapper.Map<Game>(updatedGameEntity);

            return game;
        }

        public virtual PropertyValueMapType GetPropertyValueMap(JObject gameFragment, Game gameContainingUpdateData)
        {
            // detirmine the names of the properties that need to be updated
            var namesOfModifiedProperties = _updateablePropertyDetector
                .GetNamesOfPropertiesToUpdate<Game>(gameFragment)
                .ToList();

            var propertyInfos = typeof (Game).GetProperties();

            var updatedPropertyValueMap = new PropertyValueMapType();

            // for each of these properties get the corresponding value and add it to the map
            foreach (var propertyName in namesOfModifiedProperties)
            {
                var propertyValue = propertyInfos.Single(x => x.Name == propertyName).GetValue(gameContainingUpdateData);
                updatedPropertyValueMap.Add(propertyName, propertyValue);
            }

            return updatedPropertyValueMap;
        }
    }
}