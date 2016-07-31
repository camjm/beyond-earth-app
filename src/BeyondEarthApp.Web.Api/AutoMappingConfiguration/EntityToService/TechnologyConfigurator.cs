using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class TechnologyConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Technology, Models.Technology>()
                .ForMember(x => x.Links, opt => opt.Ignore())
                .ForMember(x => x.Units, opt => opt.ResolveUsing<TechnologyUnitsResolver>())
                .ForMember(x => x.Buildings, opt => opt.ResolveUsing<TechnologyBuildingsResolver>())
                .ForMember(x => x.TechnologyAffinities, opt => opt.ResolveUsing<TechnologyAffinitiesResolver>());

            // Precis mapping
            mapper.CreateMap<Technology, Models.Precis.TechnologyPrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}