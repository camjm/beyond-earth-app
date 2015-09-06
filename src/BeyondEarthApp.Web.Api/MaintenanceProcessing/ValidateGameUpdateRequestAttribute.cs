using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Common;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// JSON-specific implementation. Filters out bad requests before reaching the controller.
    /// </summary>
    public class ValidateGameUpdateRequestAttribute : ActionFilterAttribute
    {
        private readonly ILog _log;

        public ValidateGameUpdateRequestAttribute() : this(WebContainerManager.Get<ILogManager>()) { }

        public ValidateGameUpdateRequestAttribute(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(ValidateGameUpdateRequestAttribute));
        }

        public override bool AllowMultiple
        {
            get { return false; }
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            // note: to handle other formats (eg xml) examine actionContext.Request.Content).Headers.ContentType

            var gameId = (long) actionContext.ActionArguments[ActionParameterNames.GameId];
            var gameFragment = (JObject) actionContext.ActionArguments[ActionParameterNames.GameFragment];

            _log.DebugFormat("{0} = {1}", ActionParameterNames.GameFragment, gameFragment);

            if (gameFragment == null)
            {
                const string errorMessage = "Malformed or null request.";
                _log.Debug(errorMessage);

                // prevents processing from reaching the controller's action method 
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);

                return;
            }

            try
            {
                var game = gameFragment.ToObject<Game>();

                if (game.GameId.HasValue && game.GameId != gameId)
                {
                    const string errorMessage = "Game Ids do not match";
                    _log.Debug(errorMessage);

                    // prevents processing from reaching the controller's action method
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, errorMessage);
                }
            }
            catch (JsonException ex)
            {
                _log.Debug(ex.Message);

                // prevents processing from reaching the controller's action method
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }

            // Request is considered valid, and controller action method invoked, if actionContext.Response has not been set.
        }
    }

    public static class ActionParameterNames
    {
        public const string GameFragment = "updatedGame";

        public const string GameId = "id";
    }
}