using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using Building = BeyondEarthApp.Web.Api.Models.Building;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public class TechnologyBuildingsResolver : ListResolver<Technology, Building>
    {
        protected override List<Building> ResolveCore(Technology source, IAutoMapper mapper)
        {
            return source.Buildings.Select(mapper.Map<Building>).ToList();
        }
    }
}