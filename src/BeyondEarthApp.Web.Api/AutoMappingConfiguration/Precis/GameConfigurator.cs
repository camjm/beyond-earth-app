using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Precis
{
    public class GameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Game, Models.Precis.GamePrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore())
                //TODO: dummy data
                .ForMember(x => x.CitiesCount, opts => opts.UseValue<int>(10))
                .ForMember(x => x.Supremacy, opts => opts.UseValue<short>(12))
                .ForMember(x => x.Harmony, opts => opts.UseValue<short>(3))
                .ForMember(x => x.Purity, opts => opts.UseValue<short>(7));
        }
    }
}