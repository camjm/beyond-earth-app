using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class BuildingConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Building, Data.Entities.Building>()
                .ForMember(opt => opt.Version, x => x.Ignore());

            // Precis mapping
            mapper.CreateMap<BuildingPrecis, Data.Entities.Building>()
                .ForMember(opt => opt.Technology, x => x.Ignore())
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}