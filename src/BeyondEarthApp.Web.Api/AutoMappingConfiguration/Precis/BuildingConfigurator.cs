using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Precis
{
    public class BuildingConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Building, Models.Precis.BuildingPrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}