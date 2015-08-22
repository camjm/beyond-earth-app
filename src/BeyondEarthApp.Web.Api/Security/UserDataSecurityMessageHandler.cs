using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Web.Api.Models;
using log4net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace BeyondEarthApp.Web.Api.Security
{
    public class UserDataSecurityMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public UserDataSecurityMessageHandler(ILogManager logManager, IUserSession userSession)
        {
            _userSession = userSession;
            _log = logManager.GetLog(typeof (UserDataSecurityMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // passes request down chain first
            var response = await base.SendAsync(request, cancellationToken);

            if (CanHandleResponse(response))
            {
                ApplySecurityToResponseData((ObjectContent) response.Content);
            }

            return response;
        }

        /// <summary>
        /// Response contains a User service model object
        /// </summary>
        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;

            var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof (User);

            return canHandleResponse;
        }

        public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var removeSensitiveData = !_userSession.IsInRole(Constants.RoleNames.Admin);

            if (removeSensitiveData)
            {
                _log.DebugFormat("Applying security data masking for user {0}", _userSession.Username);
            }

            // By convention, ASP.NET Web API uses reflection to call ShouldSerialize* methods to determine if specific properties should be serialized
            ((User) responseObjectContent.Value).SetSerializeGames(!removeSensitiveData);
        }
    }
}