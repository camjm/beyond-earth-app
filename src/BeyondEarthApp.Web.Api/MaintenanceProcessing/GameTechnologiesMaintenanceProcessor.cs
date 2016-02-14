using System.Collections.Generic;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public class GameTechnologiesMaintenanceProcessor : IGameTechnologiesMaintenceProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameLinkService _gameLinkService;
        private readonly IUpdateGameQueryProcessor _queryProcessor;

        public GameTechnologiesMaintenanceProcessor(
            IAutoMapper autoMapper, 
            IGameLinkService gameLinkService,
            IUpdateGameQueryProcessor queryProcessor)
        {
            _autoMapper = autoMapper;
            _queryProcessor = queryProcessor;
            _gameLinkService = gameLinkService;
        }

        public Game ReplaceGameTechnologies(long gameId, IEnumerable<long> technologyIds)
        {
            var gameEntity = _queryProcessor.ReplaceGameTechnologies(gameId, technologyIds);

            return CreateGameResponse(gameEntity);
        }

        public Game DeleteGameTechnologies(long gameId)
        {
            var gameEntity = _queryProcessor.DeleteGameTechnologies(gameId);

            return CreateGameResponse(gameEntity);
        }

        public Game AddGameTechnology(long gameId, long technologyId)
        {
            var gameEntity = _queryProcessor.AddGameTechnology(gameId, technologyId);

            return CreateGameResponse(gameEntity);
        }

        public Game DeleteGameTechnology(long gameId, long technologyId)
        {
            var gameEntity = _queryProcessor.DeleteGameTechnology(gameId, technologyId);

            return CreateGameResponse(gameEntity);
        }

        public virtual Game CreateGameResponse(Data.Entities.Game gameEntity)
        {
            var game = _autoMapper.Map<Game>(gameEntity);

            _gameLinkService.AddLinks(game);

            return game;
        }
    }
}