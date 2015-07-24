using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class UnitConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Unit, Data.Entities.Unit>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}