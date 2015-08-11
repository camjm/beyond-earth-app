using System.Net.Http;
using System.Web.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class GamesController : ApiController
    {
        private readonly IAddGameMaintenanceProcessor _addGameMaintenanceProcessor;

        public GamesController(IAddGameMaintenanceProcessor addGameMaintenanceProcessor)
        {
            _addGameMaintenanceProcessor = addGameMaintenanceProcessor;
        }

        [Route("", Name = "AddGameRoute")]
        [HttpPost]
        [Authorize(Roles = Constants.RoleNames.User)]   // Security Principal on the current HttpContext must have User role. If unauthorized, returns a 401 HTTP status code
        public IHttpActionResult AddGame(HttpRequestMessage requestMessage, NewGame newGame)
        {
            // Delegate all work to maintenance processor
            var game = _addGameMaintenanceProcessor.AddGame(newGame);
            var result = new CreatedActionResult<Game>(game, requestMessage);
            return result;
        }
    }
}
