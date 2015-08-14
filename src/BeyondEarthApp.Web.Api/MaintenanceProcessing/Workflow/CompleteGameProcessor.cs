using BeyondEarthApp.Common;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public class CompleteGameProcessor : ICompleteGameProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly IAutoMapper _autoMapper;
        private readonly IGameByIdQueryProcessor _gameByIdQueryProcessor;
        private readonly IUpdateGameStatusQueryProcessor _updateGameStatusQueryProcessor;

        public CompleteGameProcessor(
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

        public Game CompleteGame(long gameId)
        {
            var gameEntity = _gameByIdQueryProcessor.GetGame(gameId);

            if (gameEntity == null)
            {
                throw new RootObjectNotFoundException("Game not found");
            }

            // simulate some workflow logic...
            if (gameEntity.Status.Name != "In Progress")
            {
                throw new BusinessRuleViolationException("Incorrect game status. Expected status of 'In Progress'.");
            }

            //gameEntity.CompletedDate = _dateTime.UtcNow;

            _updateGameStatusQueryProcessor.UpdateGameStatus(gameEntity, "Completed");

            var game = _autoMapper.Map<Game>(gameEntity);

            return game;
        }
    }
}