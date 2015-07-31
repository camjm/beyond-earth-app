using System.Collections.Generic;
using AutoMapper;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Common;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public abstract class ListResolver<TSource, TDestination> : ValueResolver<TSource, List<TDestination>>
    {
        private IAutoMapper AutoMapper
        {
            get { return WebContainerManager.Get<IAutoMapper>(); }
        }

        protected override List<TDestination> ResolveCore(TSource source)
        {
            return ResolveCore(source, AutoMapper);
        }

        protected abstract List<TDestination> ResolveCore(TSource source, IAutoMapper mapper);
    }
}
