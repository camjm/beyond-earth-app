using System.Collections.Generic;
using System.Linq;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using Game = BeyondEarthApp.Web.Api.Models.Game;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers
{
    public class UserGameResolver : ListResolver<User, Game>
    {
        protected override List<Game> ResolveCore(User source, IAutoMapper mapper)
        {
            return source.Games.Select(mapper.Map<Game>).ToList();
        }
    }
}