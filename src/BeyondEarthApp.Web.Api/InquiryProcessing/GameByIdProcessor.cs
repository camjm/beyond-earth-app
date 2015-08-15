using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.InquiryProcessing
{
    public class GameByIdProcessor : IGameByIdProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameByIdQueryProcessor _gameByIdQueryProcessor;

        public GameByIdProcessor(
            IAutoMapper autoMapper, 
            IGameByIdQueryProcessor gameByIdQueryProcessor)
        {
            _autoMapper = autoMapper;
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

            return game;
        }
    }
}