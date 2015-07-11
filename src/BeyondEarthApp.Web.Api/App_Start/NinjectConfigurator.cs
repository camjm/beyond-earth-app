using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using log4net.Config;
using Ninject;

namespace BeyondEarthApp.Web.Api
{
    /// <summary>
    /// Container Bindings.
    /// Relate interfaces to concrete implementations so dependencies can be resolved at runtime.
    /// </summary>
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }

        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);
            ConfigureNHibernate(container);
            ConfigureUserSession(container);
            ConfigureAutoMapper(container);

            container
                .Bind<IDateTime>()
                .To<DateTimeAdapter>()
                .InSingletonScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            //TODO
        }

        private void ConfigureUserSession(IKernel container)
        {
            //TODO
        }

        private void ConfigureNHibernate(IKernel container)
        {
            //TODO
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            var logManager = new LogManagerAdapter();
            container
                .Bind<ILogManager>()
                .ToConstant(logManager);
        }
    }
}