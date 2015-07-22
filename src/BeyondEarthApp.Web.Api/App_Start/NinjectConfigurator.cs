using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.SqlServer.Mapping;
using BeyondEarthApp.Data.SqlServer.QueryProcessors;
using BeyondEarthApp.Web.Common;
using BeyondEarthApp.Web.Common.Security;
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

            //Singleton: shared instance for the entire lifetime of the application
            container
                .Bind<IDateTime>()
                .To<DateTimeAdapter>()
                .InSingletonScope();

            container
                .Bind<IAddTechnologyQueryProcessor>()
                .To<AddTechnologyQueryProcessor>()
                .InRequestScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            //TODO
        }

        private void ConfigureUserSession(IKernel container)
        {
            // UserSession is just a wrapper, does not store state, so can be singleton
            var userSession = new UserSession();
            container
                .Bind<IUserSession>()
                .ToConstant(userSession)
                .InSingletonScope();
            container
                .Bind<IWebUserSession>()
                .ToConstant(userSession)
                .InSingletonScope();
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

            // Only ever create a single ISession instance per request
            container
                .Bind<ISession>()
                .ToMethod(CreateSession)
                .InRequestScope();

            container
                .Bind<IActionTransactionHelper>()
                .To<ActionTransactionHelper>()
                .InRequestScope();
        }

        private ISession CreateSession(IContext context)
        {
            // CurrentSessionContext binds to underlying ASP.NET's HttpContext (see .CurrentSessionContext("web") above) which will manage ISession instances
            // Each new web request will have a different HttpContext and open instance of ISession
            var sessionFactory = context.Kernel.Get<ISessionFactory>();
            if (!CurrentSessionContext.HasBind(sessionFactory))
            {
                var session = sessionFactory.OpenSession(); // Open connection to database
                CurrentSessionContext.Bind(session);
            }

            return sessionFactory.GetCurrentSession();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();

            //ToConstant: for application-level singleton that our code, not Ninject, instansiates
            var logManager = new LogManagerAdapter();
            container
                .Bind<ILogManager>()
                .ToConstant(logManager);
        }
    }
}