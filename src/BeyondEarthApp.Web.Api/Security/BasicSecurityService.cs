using System;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Web;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Common;
using log4net;
using NHibernate;

namespace BeyondEarthApp.Web.Api.Security
{
    public class BasicSecurityService : IBasicSecurityService
    {
        private readonly ILog _log;

        /// <summary>
        /// ISession cannot be constructor-injected because this constructor is called in the application start-up before ISession has been prepared, not in response to a web request
        /// </summary>
        public BasicSecurityService(ILogManager logManager)
        {
            _log = logManager.GetLog(typeof(BasicSecurityService));
        }

        /// <summary>
        /// Access to ISession by lazy loading. By the time this property is actually used, ISession is available as a dependency.
        /// </summary>
        public virtual ISession Session
        {
            get { return WebContainerManager.Get<ISession>(); }
        }

        /// <summary>
        /// Sets up a security principal object on the current HTTP context that contains the user's indentity and claims
        /// </summary>
        public bool SetPrincipal(string username, string password)
        {
            var user = GetUser(username);

            IPrincipal principal;
            if (user == null || (principal = GetPrincipal(user)) == null)
            {
                _log.DebugFormat("System could not validate user {0}", username);
                return false;
            }

            // Associate Principal with the current context. Each web request executes in its own HTTP context, but can be processed by multiple threads.
            Thread.CurrentPrincipal = principal;    // legacy
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }

            return true;
        }

        /// <summary>
        /// Builds the Principal
        /// </summary>
        public virtual IPrincipal GetPrincipal(User user)
        {
            var identity = new GenericIdentity(user.UserName, Constants.SchemeTypes.Basic);

            identity.AddClaim(new Claim(ClaimTypes.GivenName, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));
            identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.User));

            var username = user.UserName.ToLowerInvariant();
            if (username == "bhogg")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, Constants.RoleNames.Admin));
            }

            return new ClaimsPrincipal(identity);
        }

        public virtual User GetUser(string username)
        {
            //TODO: verify password
            username = username.ToLowerInvariant();

            return Session.QueryOver<User>()
                .Where(x => x.UserName == username)
                .SingleOrDefault();
        }
    }
}