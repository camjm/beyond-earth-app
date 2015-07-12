using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Data.SqlServer.Mapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using log4net.Config;
using NHibernate;
using NHibernate.Context;
using Ninject;
using Ninject.Activation;
using Ninject.Web.Common;

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
            var sessionFactory = Fluently.Configure()
                .Database(
                    MsSqlConfiguration.MsSql2008.ConnectionString(c =>                  // SQL Server version compatible with 2012
                        c.FromConnectionStringWithKey("BeyondEarthAppDb")))             // Load connection string from web.config
                .CurrentSessionContext("web")                                           // Session management: one database session per web request
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<TechnologyMap>())     // Mappings assembly
                .BuildSessionFactory();                                                 // Create configured ISessionFactoryInstance

            // Only ever create a single ISessionFactory instance per application
            container
                .Bind<ISessionFactory>()
                .ToConstant(sessionFactory);
            container
                .Bind<ISession>()
                .ToMethod(CreateSession)
                .InRequestScope();
        }

        private ISession CreateSession(IContext context)
        {
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession();
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
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