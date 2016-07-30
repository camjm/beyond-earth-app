using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Precis
{
    public class GameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Game, Models.Precis.GamePrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}