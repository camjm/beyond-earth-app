using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class TechnologyConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Technology, Models.Technology>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Buildings, x => x.ResolveUsing<TechnologyBuildingsResolver>())
                .ForMember(opt => opt.Units, x => x.ResolveUsing<TechnologyUnitsResolver>());
        }
    }
}