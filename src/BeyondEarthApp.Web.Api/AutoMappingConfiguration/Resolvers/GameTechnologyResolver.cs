using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using Technology = BeyondEarthApp.Web.Api.Models.Technology;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public class GameTechnologyResolver : ListResolver<Game, Technology>
    {
        protected override List<Technology> ResolveCore(Game source, IAutoMapper mapper)
        {
            return source.Technologies.Select(mapper.Map<Technology>).ToList();
        }
    }
}