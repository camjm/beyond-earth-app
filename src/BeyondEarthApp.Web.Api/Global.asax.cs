using System.Web.Http;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Security;
using BeyondEarthApp.Web.Common;

namespace BeyondEarthApp.Web.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            RegisterHandlers();

            new AutoMapperConfigurator().Configure(
                WebContainerManager.Get<IAutoMapper>(), 
                WebContainerManager.GetAll<IAutoMapperTypeConfigurator>());
        }

        /// <summary>
        /// Adds handlers to the application's message-handler pipeline
        /// </summary>
        private void RegisterHandlers()
        {
            var logManager = WebContainerManager.Get<ILogManager>();
            var userSession = WebContainerManager.Get<IUserSession>();
            var securityService = WebContainerManager.Get<IBasicSecurityService>();

            GlobalConfiguration.Configuration.MessageHandlers.Add(new BasicAuthenticationMessageHandler(logManager, securityService));
            GlobalConfiguration.Configuration.MessageHandlers.Add(new UserDataSecurityMessageHandler(logManager, userSession));
        }

        /// <summary>
        /// Handle the base class HttpApplication error event. 
        /// Log exceptions that occur during the application start-up, before the custom exception 
        /// logger and global exception handling configurations have been initialized.
        /// </summary>
        protected void Application_Error()
        {
            var exception = Server.GetLastError();
            if (exception != null)
            {
                var log = WebContainerManager.Get<ILogManager>().GetLog(typeof (WebApiApplication));
                log.Error("Unhandled Exception", exception);
            }
        }
    }
}
