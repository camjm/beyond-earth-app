using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using Unit = BeyondEarthApp.Web.Api.Models.Unit;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public class TechnologyUnitsResolver : ListResolver<Technology, Unit>
    {
        protected override List<Unit> ResolveCore(Technology source, IAutoMapper mapper)
        {
            return source.Units.Select(mapper.Map<Unit>).ToList();
        }
    }
}