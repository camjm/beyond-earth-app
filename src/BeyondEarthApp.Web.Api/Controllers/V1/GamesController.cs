using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BeyondEarthApp.Common;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;
using BeyondEarthApp.Web.Api.Models.Paging;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using BeyondEarthApp.Web.Common.Validation;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    [EnableCors("http://127.0.0.1:8080", "*", "*")]
    public class GamesController : ApiController
    {
        private readonly IPagedDataRequestFactory _pagedDataRequestFactory;
        private readonly IUpdateGameMaintenanceProcessor _updateGameMaintenanceProcessor;
        private readonly IAddGameMaintenanceProcessor _addGameMaintenanceProcessor;
        private readonly IDeleteGameQueryProcessor _deleteGameQueryProcessor;
        private readonly IGameByIdProcessor _gameByIdProcessor;
        private readonly IAllGamesProcessor _allGamesProcessor;

        public GamesController(
            IPagedDataRequestFactory pagedDataRequestFactory,
            IUpdateGameMaintenanceProcessor updateGameMaintenanceProcessor,
            IAddGameMaintenanceProcessor addGameMaintenanceProcessor, 
            IDeleteGameQueryProcessor deleteGameQueryProcessor,
            IGameByIdProcessor gameByIdProcessor,
            IAllGamesProcessor allGamesProcessor)
        {
            _pagedDataRequestFactory = pagedDataRequestFactory;
            _updateGameMaintenanceProcessor = updateGameMaintenanceProcessor;
            _addGameMaintenanceProcessor = addGameMaintenanceProcessor;
            _deleteGameQueryProcessor = deleteGameQueryProcessor;
            _gameByIdProcessor = gameByIdProcessor;
            _allGamesProcessor = allGamesProcessor;
        }

        [HttpGet]
        [Route("{id:long}", Name = "GetGameRoute")]
        public Game GetGame(long id)
        {
            var game = _gameByIdProcessor.GetGame(id);
            return game;
        }

        [HttpGet]
        [Route("", Name = "GetGamesRoute")]
        public PagedDataInquiryResponse<Game> GetGames(HttpRequestMessage requestMessage)
        {
            var request = _pagedDataRequestFactory.Create(requestMessage.RequestUri);
            var games = _allGamesProcessor.GetGames(request);
            return games;
        }

        [Route("", Name = "AddGameRoute")]
        [HttpPost]
        [ValidateModel]
        public IHttpActionResult AddGame(HttpRequestMessage requestMessage, NewGame newGame)
        {
            // Delegate all work to maintenance processor
            var game = _addGameMaintenanceProcessor.AddGame(newGame);
            var result = new CreatedActionResult<Game>(game, requestMessage);
            return result;
        }

        [HttpPut]
        [HttpPatch]
        [ValidateGameUpdateRequest]
        [Route("{id:long}", Name = "UpdateGameRoute")]
        public Game UpdateGame(long id, [FromBody] object updatedGame)
        {
            // object type makes partial updates possible by allowing a sparse representation of Game. 
            // If ASP.NET Web API model binding was used, we wouldn't know what the caller wanted to partially update.
            var game = _updateGameMaintenanceProcessor.UpdateGame(id, updatedGame);
            return game;
        }

        [HttpDelete]
        [Route("{id:long}", Name = "DeleteGameRoute")]
        public IHttpActionResult DeleteGame(long id)
        {
            _deleteGameQueryProcessor.DeleteGame(id);
            return Ok();
        }
    }
}
