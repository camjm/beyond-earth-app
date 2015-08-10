using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using log4net;

namespace BeyondEarthApp.Web.Api.Security
{
    /// <summary>
    /// Authentication Message Handler (Invoked before the filters and controller actions). Responsible for 
    /// building a principal and associating it with the current context, before the authorization filter.
    /// </summary>
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        private readonly ILog _log;
        private readonly IBasicSecurityService _basicServiceService;

        public const char AuthorizationHeaderSeparator = ':';
        private const int UsernameIndex = 0;
        private const int PasswordIndex = 1;
        private const int ExpectedCredentialCount = 2;

        public BasicAuthenticationMessageHandler(
            ILogManager logManager, 
            IBasicSecurityService basicSecurityService)
        {
            _basicServiceService = basicSecurityService;
            _log = logManager.GetLog(typeof (BasicAuthenticationMessageHandler));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                _log.Debug("Already authenticated; passing on to next handler..");
                return await base.SendAsync(request, cancellationToken);
            }

            if (!CanHandleAuthentication(request))
            {
                _log.Debug("Not a basic auth request; passing on to next handler...");
                return await base.SendAsync(request, cancellationToken);
            }

            bool isAuthenticated;
            try
            {
                isAuthenticated = Authenticate(request);
            }
            catch (Exception e)
            {
                _log.Error("Failure in auth processing", e);
                return CreateUnauthorizedResponse();
            }

            if (isAuthenticated)
            {
                // pass request ot the next handler in the processing pipeline
                var response = await base.SendAsync(request, cancellationToken);

                return response.StatusCode == HttpStatusCode.Unauthorized
                    ? CreateUnauthorizedResponse()
                    : response;
            }

            // error response returned to requester
            return CreateUnauthorizedResponse();
        }

        public bool CanHandleAuthentication(HttpRequestMessage request)
        {
            return (
                request.Headers != null && 
                request.Headers.Authorization != null && 
                request.Headers.Authorization.Scheme.ToLowerInvariant() == Constants.SchemeTypes.Basic);
        }

        /// <summary>
        /// Examines Authorization Header
        /// </summary>
        public bool Authenticate(HttpRequestMessage request)
        {
            _log.Debug("Attempting to authenticate...");

            var authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return false;
            }

            var credentialParts = GetCredentialParts(authHeader);
            if (credentialParts.Length != ExpectedCredentialCount)
            {
                return false;
            }

            // Delegate setting the principal to service
            return _basicServiceService.SetPrincipal(
                credentialParts[UsernameIndex], 
                credentialParts[PasswordIndex]);
        }

        public string[] GetCredentialParts(AuthenticationHeaderValue authHeader)
        {
            var encodedCredentials = authHeader.Parameter;
            var credentialBytes = Convert.FromBase64String(encodedCredentials);
            var credentials = Encoding.ASCII.GetString(credentialBytes);
            var credentialParts = credentials.Split(AuthorizationHeaderSeparator);
            return credentialParts;
        }

        public HttpResponseMessage CreateUnauthorizedResponse()
        {
            // this response will trigger most browsers to prompt for a username and password 
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(new AuthenticationHeaderValue(Constants.SchemeTypes.Basic));

            return response;
        }
    }
}