using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    public class GamesController : ApiController
    {
        private readonly IAddGameMaintenanceProcessor _addGameMaintenanceProcessor;

        public GamesController(IAddGameMaintenanceProcessor addGameMaintenanceProcessor)
        {
            _addGameMaintenanceProcessor = addGameMaintenanceProcessor;
        }

        [Route("", Name = "AddGameRoute")]
        [HttpPost]
        public IHttpActionResult AddGame(HttpRequestMessage requestMessage, NewGame newGame)
        {
            // Delegate all work to maintenance processor
            var game = _addGameMaintenanceProcessor.AddGame(newGame);
            var result = new CreatedActionResult<Game>(game, requestMessage);
            return result;
        }
    }
}
