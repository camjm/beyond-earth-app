using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class AddGameMaintenanceProcessor : IAddGameMaintenanceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IAddGameQueryProcessor _queryProcessor;

        public AddGameMaintenanceProcessor(
            IAutoMapper autoMapper, 
            IAddGameQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
        }

        public Game AddGame(NewGame newGame)
        {
            // Map service model to entity model
            var gameEntity = _autoMapper.Map<Data.Entities.Game>(newGame);

            // Persist entity model
            _queryProcessor.AddGame(gameEntity);

            // Map new entity model back to full service model
            var game = _autoMapper.Map<Game>(gameEntity);

            //TODO: implement Link Service
            game.AddLink(new Link
            {
                Method = HttpMethod.Get.Method,
                Href = "http://localhost:52204/api/v1/games/" + game.GameId,
                Rel = Constants.CommonLinkRelValues.Self
            });

            return game;
        }
    }
}