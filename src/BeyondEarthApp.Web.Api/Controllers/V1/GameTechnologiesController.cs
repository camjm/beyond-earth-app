using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Routing;
using System.Collections.Generic;
using System.Web.Http;

namespace BeyondEarthApp.Web.Api.Controllers.V1
{
    [ApiVersion1RoutePrefix("games")]
    [UnitOfWorkActionFilter]
    [Authorize(Roles = Constants.RoleNames.User)]
    public class GameTechnologiesController : ApiController
    {
        private readonly IGameTechnologiesMaintenceProcessor _gameTechnologiesMaintenceProcessor;

        public GameTechnologiesController(IGameTechnologiesMaintenceProcessor gameTechnologiesMaintenceProcessor)
        {
            _gameTechnologiesMaintenceProcessor = gameTechnologiesMaintenceProcessor;
        }

        [HttpPut]
        [Route("{gameId:long}/technologies", Name = "ReplaceGameTechnologiesRoute")]
        public Game ReplaceGameTechnologies(long gameId, [FromBody] IEnumerable<long> technologyIds)
        {
            var game = _gameTechnologiesMaintenceProcessor.ReplaceGameTechnologies(gameId, technologyIds);
            return game;
        }

        [HttpDelete]
        [Route("{gameId:long}/technologies", Name = "DeleteGameTechnologiesRoute")]
        public Game DeleteGameTechnologies(long gameId)
        {
            var game = _gameTechnologiesMaintenceProcessor.DeleteGameTechnologies(gameId);
            return game;
        }

        [HttpPut]
        [Route("{gameId:long}/technologies/{technologyId:long}", Name = "AddGameTechnologyRoute")]
        public Game AddGameTechnology(long gameId, long technologyId)
        {
            var game = _gameTechnologiesMaintenceProcessor.AddGameTechnology(gameId, technologyId);
            return game;
        }

        [HttpDelete]
        [Route("{gameId:long}/technologies/{technologyId:long}", Name = "DeleteGameTechnologyRoute")]
        public Game DeleteGameTechnology(long gameId, long technologyId)
        {
            var game = _gameTechnologiesMaintenceProcessor.DeleteGameTechnology(gameId, technologyId);
            return game;
        }
    }
}
