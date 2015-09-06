using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using BeyondEarthApp.Web.Common.Validation;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class GamesController : ApiController
    {
        private readonly IUpdateGameMaintenanceProcessor _updateGameMaintenanceProcessor;
        private readonly IAddGameMaintenanceProcessor _addGameMaintenanceProcessor;
        private readonly IGameByIdProcessor _gameByIdProcessor;

        public GamesController(
            IUpdateGameMaintenanceProcessor updateGameMaintenanceProcessor,
            IAddGameMaintenanceProcessor addGameMaintenanceProcessor, 
            IGameByIdProcessor gameByIdProcessor)
        {
            _updateGameMaintenanceProcessor = updateGameMaintenanceProcessor;
            _addGameMaintenanceProcessor = addGameMaintenanceProcessor;
            _gameByIdProcessor = gameByIdProcessor;
        }

        [Route("", Name = "AddGameRoute")]
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = Constants.RoleNames.User)]   //DONT NEED THIS Security Principal on the current HttpContext must have User role. If unauthorized, returns a 401 HTTP status code
        public IHttpActionResult AddGame(HttpRequestMessage requestMessage, NewGame newGame)
        {
            // Delegate all work to maintenance processor
            var game = _addGameMaintenanceProcessor.AddGame(newGame);
            var result = new CreatedActionResult<Game>(game, requestMessage);
            return result;
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetGameRoute")]
        public Game GetGame(long id)
        {
            var game = _gameByIdProcessor.GetGame(id);
            return game;
        }

        [HttpPut]
        [HttpPatch]
        [ValidateGameUpdateRequest]
        [Authorize(Roles = Constants.RoleNames.User)]
        [Route("{id:long}", Name = "UpdateGameRoute")]
        public Game UpdateGame(long id, [FromBody] object updatedGame) 
        {
            // object type makes partial updates possible by allowing a sparse representation of Game. 
            // If ASP.NET Web API model binding was used, we wouldn't know what the caller wanted to partially update.
            var game = _updateGameMaintenanceProcessor.UpdateGame(id, updatedGame);
            return game;
        }
    }
}
