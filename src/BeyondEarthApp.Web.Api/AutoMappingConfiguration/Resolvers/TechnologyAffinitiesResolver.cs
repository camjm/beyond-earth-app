using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using TechnologyAffinity = BeyondEarthApp.Web.Api.Models.TechnologyAffinity;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public class TechnologyAffinitiesResolver : ListResolver<Technology, TechnologyAffinity>
    {
        protected override List<TechnologyAffinity> ResolveCore(Technology source, IAutoMapper mapper)
        {
            return source.TechnologyAffinities.Select(mapper.Map<TechnologyAffinity>).ToList();
        }
    }
}