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
    /// <summary>
    /// JSON-specific implementation.
    /// </summary>
    public class UpdateGameMaintenanceProcessor : IUpdateGameMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameLinkService _gameLinkService;
        private readonly IUpdateGameQueryProcessor _queryProcessor;
        private readonly IUpdateablePropertyDetector _updateablePropertyDetector;

        public UpdateGameMaintenanceProcessor(
            IAutoMapper autoMapper, 
            IGameLinkService gameLinkService,
            IUpdateGameQueryProcessor queryProcessor, 
            IUpdateablePropertyDetector updateablePropertyDetector)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _gameLinkService = gameLinkService;
            _updateablePropertyDetector = updateablePropertyDetector;
        }

        /// <summary>
        /// Uses JObject to parse the game fragment into an actual Game instance, persists the updates, and maps the returned entity to the service model.
        /// </summary>
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

            _gameLinkService.AddLinks(game);

            return game;
        }

        /// <summary>
        /// Creates the PropertyValueMap instance and populates it with the new values.
        /// </summary>
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