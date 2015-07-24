using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Common;
using Building = BeyondEarthApp.Web.Api.Models.Building;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class TechnologyBuildingsResolver : ValueResolver<Technology, List<Building>>
    {
        public IAutoMapper AutoMapper
        {
            get { return WebContainerManager.Get<IAutoMapper>(); }
        }

        protected override List<Building> ResolveCore(Technology source)
        {
            return source.Buildings.Select(x => AutoMapper.Map<Building>(x)).ToList();
        }
    }
}