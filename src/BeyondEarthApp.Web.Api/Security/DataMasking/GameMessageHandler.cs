using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Web.Api.Models;
using log4net;

namespace BeyondEarthApp.Web.Api.Security.DataMasking
{
    public class GameMessageHandler : DataSecurityMessageHandler<Game>
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public GameMessageHandler(ILogManager logManager, IUserSession userSession)
        {
            _userSession = userSession;
            _log = logManager.GetLog(typeof(GameMessageHandler));
        }

        public override void ApplySecurityToResponseData(Game game, ObjectContent responseObjectContent)
        {
            var removeSensitiveData = !_userSession.IsInRole(Constants.RoleNames.Admin);

            if (removeSensitiveData)
            {
                _log.DebugFormat("Applying security data masking for user {0}", _userSession.Username);
            }

            // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to determine if specific properties should be serialized
            game.SetSerializeTechnologies(!removeSensitiveData);
        }
    }
}