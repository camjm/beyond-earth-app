using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Web.Api.Models;
using log4net;

namespace BeyondEarthApp.Web.Api.Security
{
    public class PagedGameDataSecurityMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;
        private readonly IUserSession _userSession;

        public PagedGameDataSecurityMessageHandler(
            ILogManager logManager, 
            IUserSession userSession)
        {
            _log = logManager.GetLog(typeof (PagedGameDataSecurityMessageHandler));
            _userSession = userSession;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (CanHandleResponse(response))
            {
                ApplySecurityToResponseData((ObjectContent) response.Content);
            }

            return response;
        }

        public bool CanHandleResponse(HttpResponseMessage response)
        {
            var objectContent = response.Content as ObjectContent;

            var canHandleResponse = objectContent != null && objectContent.ObjectType == typeof (PagedDataInquiryResponse<Game>);

            return canHandleResponse;
        }

        public void ApplySecurityToResponseData(ObjectContent responseObjectContent)
        {
            var maskData = !_userSession.IsInRole(Constants.RoleNames.Admin);

            if (maskData)
            {
                _log.DebugFormat("Applying security data masking for user {0}", _userSession.Username);
            }

            ((PagedDataInquiryResponse<Game>) responseObjectContent.Value).Items.ForEach(x => x.SetSerializeTechnologies(!maskData));
        }
    }
}