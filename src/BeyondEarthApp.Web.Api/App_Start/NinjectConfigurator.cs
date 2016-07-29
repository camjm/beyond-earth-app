using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Logging;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.SqlServer.Mapping;
using BeyondEarthApp.Data.SqlServer.QueryProcessors;
using BeyondEarthApp.Web.Api.InquiryProcessing;
using BeyondEarthApp.Web.Api.LinkServices;
using BeyondEarthApp.Web.Api.MaintenanceProcessing;
using BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow;
using BeyondEarthApp.Web.Api.Security;
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
using EntityToService = BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService;
using ServiceToEntity = BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity;

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
            ConfigureQueryProcessors(container);
            ConfigureMaintenanceProcessors(container);
            ConfigureInquiryProcessors(container);
            ConfigureLinkServices(container);

            // Singleton: shared instance for the entire lifetime of the application
            container
                .Bind<IDateTime>()
                .To<DateTimeAdapter>()
                .InSingletonScope();

            // Singleton: stores not state except an ILog instance, which is multithread safe
            container
                .Bind<IBasicSecurityService>()
                .To<BasicSecurityService>()
                .InSingletonScope();

            container
                .Bind<IUpdateablePropertyDetector>()
                .To<JObjectUpdateablePropertyDetector>()
                .InSingletonScope();

            container
                .Bind<IPagedDataRequestFactory>()
                .To<PagedDataRequestFactory>()
                .InSingletonScope();
        }

        private void ConfigureQueryProcessors(IKernel container)
        {
            // Add
            container
                .Bind<IAddTechnologyQueryProcessor>()
                .To<AddTechnologyQueryProcessor>()
                .InRequestScope();

            container
                .Bind<IAddGameQueryProcessor>()
                .To<AddGameQueryProcessor>()
                .InRequestScope();

            // Get
            container
                .Bind<IGameByIdQueryProcessor>()
                .To<GameByIdQueryProcessor>()
                .InRequestScope();

            container
                .Bind<ITechnologyByIdQueryProcessor>()
                .To<TechnologyByIdQueryProcessor>()
                .InRequestScope();


            // Update
            container
                .Bind<IUpdateGameStatusQueryProcessor>()
                .To<UpdateGameStatusQueryProcessor>()
                .InRequestScope();

            container
                .Bind<IUpdateGameQueryProcessor>()
                .To<UpdateGameQueryProcessor>()
                .InRequestScope();

            // Delete
            container
                .Bind<IDeleteGameQueryProcessor>()
                .To<DeleteGameQueryProcessor>()
                .InRequestScope();

            // All
            container
                .Bind<IAllGamesQueryProcessor>()
                .To<AllGamesQueryProcessor>()
                .InRequestScope();

            container
                .Bind<IAllStatusesQueryProcessor>()
                .To<AllStatusesQueryProcessor>()
                .InRequestScope();
        }

        private void ConfigureMaintenanceProcessors(IKernel container)
        {
            // Add
            container
                .Bind<IAddTechnologyMaintenanceProcessor>()
                .To<AddTechnologyMaintenanceProcessor>()
                .InRequestScope();

            container
                .Bind<IAddGameMaintenanceProcessor>()
                .To<AddGameMaintenanceProcessor>()
                .InRequestScope();

            // Update
            container
                .Bind<IGameTechnologiesMaintenceProcessor>()
                .To<GameTechnologiesMaintenanceProcessor>()
                .InRequestScope();

            container
                .Bind<IUpdateGameMaintenanceProcessor>()
                .To<UpdateGameMaintenanceProcessor>()
                .InRequestScope();

            // Workflow
            container
                .Bind<IStartGameProcessor>()
                .To<StartGameProcessor>()
                .InRequestScope();

            container
                .Bind<IRestartGameProcessor>()
                .To<RestartGameProcessor>()
                .InRequestScope();

            container
                .Bind<ICompleteGameProcessor>()
                .To<CompleteGameProcessor>()
                .InRequestScope();
        }

        private void ConfigureInquiryProcessors(IKernel container)
        {
            // Single
            container
                .Bind<IGameByIdProcessor>()
                .To<GameByIdProcessor>()
                .InRequestScope();

            container
                .Bind<ITechnologyByIdProcessor>()
                .To<TechnologyByIdProcessor>()
                .InRequestScope();

            // All
            container
                .Bind<IAllGamesProcessor>()
                .To<AllGamesProcessor>()
                .InRequestScope();

            container
                .Bind<IAllStatusesProcessor>()
                .To<AllStatusesProcessor>()
                .InRequestScope();
        }

        private void ConfigureLinkServices(IKernel container)
        {
            container
                .Bind<ICommonLinkService>()
                .To<CommonLinkService>()
                .InRequestScope();

            container
                .Bind<IGameLinkService>()
                .To<GameLinkService>()
                .InRequestScope();

            container
                .Bind<IFactionLinkService>()
                .To<FactionLinkService>()
                .InRequestScope();

            container
                .Bind<ITechnologyLinkService>()
                .To<TechnologyLinkService>()
                .InRequestScope();

            container
                .Bind<IBuildingLinkService>()
                .To<BuildingLinkService>()
                .InRequestScope();

            container
                .Bind<IUnitLinkService>()
                .To<UnitLinkService>()
                .InRequestScope();

            container
                .Bind<IAffinityLinkService>()
                .To<AffinityLinkService>()
                .InRequestScope();

            container
                .Bind<IStatusLinkService>()
                .To<StatusLinkService>()
                .InRequestScope();
        }

        private void ConfigureAutoMapper(IKernel container)
        {
            container
                .Bind<IAutoMapper>()
                .To<AutoMapperAdapter>()
                .InSingletonScope();

            // Map Entity to Service
            MapConfigurator<EntityToService.TechnologyConfigurator>(container);
            MapConfigurator<EntityToService.BuildingConfigurator>(container);
            MapConfigurator<EntityToService.UnitConfigurator>(container);
            MapConfigurator<EntityToService.FactionConfigurator>(container);
            MapConfigurator<EntityToService.StatusConfigurator>(container);
            MapConfigurator<EntityToService.GameConfigurator>(container);
            MapConfigurator<EntityToService.UserConfigurator>(container);

            // Map Service to Entity
            MapConfigurator<ServiceToEntity.NewTechnologyConfigurator>(container);
            MapConfigurator<ServiceToEntity.TechnologyConfigurator>(container);
            MapConfigurator<ServiceToEntity.BuildingConfigurator>(container);
            MapConfigurator<ServiceToEntity.UnitConfigurator>(container);
            MapConfigurator<ServiceToEntity.FactionConfigurator>(container);
            MapConfigurator<ServiceToEntity.StatusConfigurator>(container);
            MapConfigurator<ServiceToEntity.NewGameConfigurator>(container);
            MapConfigurator<ServiceToEntity.GameConfigurator>(container);
            MapConfigurator<ServiceToEntity.UserConfigurator>(container);
        }

        private void MapConfigurator<T>(IKernel container) where T : IAutoMapperTypeConfigurator
        {
            container
                .Bind<IAutoMapperTypeConfigurator>()
                .To<T>()
                .InSingletonScope();
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