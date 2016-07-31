using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class UnitConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Unit, Data.Entities.Unit>()
                .ForMember(opt => opt.Version, x => x.Ignore());

            mapper.CreateMap<UnitPrecis, Data.Entities.Unit>()
                .ForMember(opt => opt.Technology, x => x.Ignore())
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}