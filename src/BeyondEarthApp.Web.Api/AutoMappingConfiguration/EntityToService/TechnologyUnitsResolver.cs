using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Common;
using Unit = BeyondEarthApp.Web.Api.Models.Unit;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class TechnologyUnitsResolver : ValueResolver<Technology, List<Unit>>
    {
        public IAutoMapper AutoMapper
        {
            get { return WebContainerManager.Get<IAutoMapper>(); }
        }

        protected override List<Unit> ResolveCore(Technology source)
        {
            return source.Units.Select(x => AutoMapper.Map<Unit>(x)).ToList();
        }
    }
}