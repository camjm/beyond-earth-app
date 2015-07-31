using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class GameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Game, Models.Game>()
                .ForMember(opts => opts.Links, x => x.Ignore())
                .ForMember(opts => opts.Technologies, x => x.ResolveUsing<GameTechnologyResolver>());
        }
    }
}