using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using BeyondEarthApp.Web.Common.Security;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    /// <summary>
    /// Non-Resource API operations. 'Conceptual' Resources, separate from Games.
    /// </summary>
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class GameWorkflowController : ApiController
    {
        private readonly IStartGameProcessor _startGameProcessor;
        private readonly ICompleteGameProcessor _completeGameProcessor;
        private readonly IRestartGameProcessor _restartGameProcessor;

        public GameWorkflowController(
            IStartGameProcessor startGameProcessor,
            ICompleteGameProcessor completeGameProcessor,
            IRestartGameProcessor restartGameProcessor)
        {
            _startGameProcessor = startGameProcessor;
            _completeGameProcessor = completeGameProcessor;
            _restartGameProcessor = restartGameProcessor;
        }

        [HttpPost]
        [Route("{gameId:long}/activations", Name = "StartGameRoute")]
        public Game StartGame(long gameId)
        {
            var game = _startGameProcessor.StartGame(gameId);
            return game;
        }

        [HttpPost]
        [Route("{gameId:long}/completions", Name = "CompleteGameRoute")]
        public Game CompleteGame(long gameId)
        {
            var game = _completeGameProcessor.CompleteGame(gameId);
            return game;
        }

        [HttpPost]
        [UserAudit]
        [Route("{gameId:long}/reactivations", Name = "RestartGameRoute")]
        public Game RestartGame(long gameId)
        {
            var game = _restartGameProcessor.RestartGame(gameId);
            return game;
        }
    }
}
