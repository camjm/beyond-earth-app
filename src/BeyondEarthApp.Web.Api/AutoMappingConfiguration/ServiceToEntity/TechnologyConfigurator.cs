using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class TechnologyConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Technology, Data.Entities.Technology>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                //.ForMember(opt => opt.TechnologyId, x => x.Ignore())
                .ForMember(opt => opt.Buildings, x => x.Ignore())
                .ForMember(opt => opt.Units, x => x.Ignore())
                .ForMember(opt => opt.TechnologyAffinities, x => x.Ignore());

            // Precis mapping
            mapper.CreateMap<TechnologyPrecis, Data.Entities.Technology>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                //.ForMember(opt => opt.TechnologyId, x => x.Ignore())
                .ForMember(opt => opt.Buildings, x => x.Ignore())
                .ForMember(opt => opt.Units, x => x.Ignore())
                .ForMember(opt => opt.TechnologyAffinities, x => x.Ignore());
        }
    }
}