using BeyondEarthApp.Common;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public class RestartGameProcessor : IRestartGameProcessor
    {
        private readonly IAutoMapper _autoMapper;
        private readonly IGameByIdQueryProcessor _gameByIdQueryProcessor;
        private readonly IUpdateGameStatusQueryProcessor _updateGameStatusQueryProcessor;

        public RestartGameProcessor(
            IAutoMapper autoMapper, 
            IGameByIdQueryProcessor gameByIdQueryProcessor, 
            IUpdateGameStatusQueryProcessor updateGameStatusQueryProcessor)
        {
            _autoMapper = autoMapper;
            _gameByIdQueryProcessor = gameByIdQueryProcessor;
            _updateGameStatusQueryProcessor = updateGameStatusQueryProcessor;
        }

        public Game RestartGame(long gameId)
        {
            var gameEntity = _gameByIdQueryProcessor.GetGame(gameId);

            if (gameEntity == null)
            {
                throw new RootObjectNotFoundException("Game not found");
            }

            // simulate some workflow logic...
            if (gameEntity.Status.Name != "Completed")
            {
                throw new BusinessRuleViolationException("Incorrect game status. Expected status of 'Completed'.");
            }

            //gameEntity.CompletedDate = null;

            _updateGameStatusQueryProcessor.UpdateGameStatus(gameEntity, "In Progress");

            var game = _autoMapper.Map<Game>(gameEntity);

            return game;
        }
    }
}