using BeyondEarthApp.Common;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public class StartGameProcessor : IStartGameProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly IAutoMapper _autoMapper;
        private readonly IGameByIdQueryProcessor _gameByIdQueryProcessor;
        private readonly IUpdateGameStatusQueryProcessor _updateGameStatusQueryProcessor;

        public StartGameProcessor(
            IDateTime dateTime, 
            IAutoMapper autoMapper, 
            IGameByIdQueryProcessor gameByIdQueryProcessor, 
            IUpdateGameStatusQueryProcessor updateGameStatusQueryProcessor)
        {
            _dateTime = dateTime;
            _autoMapper = autoMapper;
            _gameByIdQueryProcessor = gameByIdQueryProcessor;
            _updateGameStatusQueryProcessor = updateGameStatusQueryProcessor;
        }

        public Game StartGame(long gameId)
        {
            var gameEntity = _gameByIdQueryProcessor.GetGame(gameId);

            if (gameEntity == null)
            {
                throw new RootObjectNotFoundException("Game not found");
            }

            // simulate some workflow logic...
            if (gameEntity.Status.Name != "Not Started")
            {
                throw new BusinessRuleViolationException("Incorrect game status. Expected status of 'Not Started'.");
            }

            gameEntity.StartDate = _dateTime.UtcNow;

            _updateGameStatusQueryProcessor.UpdateGameStatus(gameEntity, "In Progress");

            var game = _autoMapper.Map<Game>(gameEntity);

            return game;
        }
    }
}