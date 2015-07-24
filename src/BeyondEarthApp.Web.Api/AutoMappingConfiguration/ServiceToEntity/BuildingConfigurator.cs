using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class BuildingConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Building, Data.Entities.Building>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}