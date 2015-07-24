using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class BuildingConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Building, Models.Building>()
                .ForMember(opt => opt.Links, x => x.Ignore());
        }
    }
}