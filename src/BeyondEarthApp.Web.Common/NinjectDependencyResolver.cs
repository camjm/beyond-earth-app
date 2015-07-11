using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Ninject;

namespace BeyondEarthApp.Web.Common
{
    /// <summary>
    /// Dependency Resolver.
    /// Wrapper around the Ninject container. Implements the interface that is used by ASP.NET to instansiate controllers.
    /// </summary>
    public sealed class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _container;

        public NinjectDependencyResolver(IKernel container)
        {
            _container = container;
        }

        public IKernel Container
        {
            get { return _container; }
        }

        public object GetService(Type serviceType)
        {
            return _container.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAll(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
