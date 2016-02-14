using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Paging;
using log4net;

namespace BeyondEarthApp.Web.Api.Security.DataMasking
{
    public class GamePagedMessageHandler : PagedDataSecurityMessageHandler<Game>
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public GamePagedMessageHandler(
            ILogManager logManager, 
            IUserSession userSession)
        {
            _log = logManager.GetLog(typeof(GamePagedMessageHandler));
            _userSession = userSession;
        }

        public override void ApplySecurityToResponseData(PagedDataInquiryResponse<Game> page, ObjectContent responseObjectContent)
        {
            var maskData = !_userSession.IsInRole(Constants.RoleNames.Admin);

            if (maskData)
            {
                _log.DebugFormat("Applying security data masking for user {0}", _userSession.Username);
            }

            page.Items.ForEach(x => x.SetSerializeTechnologies(!maskData));
        }
    }
}