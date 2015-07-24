using AutoMapper;
using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class NewTechnologyConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            // Ignore properties on the target class that aren't available on the source class
            mapper.CreateMap<NewTechnology, Data.Entities.Technology>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.TechnologyId, x => x.Ignore())
                .ForMember(opt => opt.Buildings, x => x.Ignore())
                .ForMember(opt => opt.Units, x => x.Ignore());
        }
    }
}