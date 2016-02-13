using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class GameByIdProcessor : IGameByIdProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameLinkService _gameLinkService;
        private readonly IGameByIdQueryProcessor _gameByIdQueryProcessor;

        public GameByIdProcessor(
            IAutoMapper autoMapper, 
            IGameLinkService gameLinkService,
            IGameByIdQueryProcessor gameByIdQueryProcessor)
        {
            _autoMapper = autoMapper;
            _gameLinkService = gameLinkService;
            _gameByIdQueryProcessor = gameByIdQueryProcessor;
        }

        public Game GetGame(long gameId)
        {
            var gameEntity = _gameByIdQueryProcessor.GetGame(gameId);

            if (gameEntity == null)
            {
                throw new RootObjectNotFoundException("Game not found");
            }

            var game = _autoMapper.Map<Game>(gameEntity);

            _gameLinkService.AddLinks(game);

            return game;
        }
    }
}