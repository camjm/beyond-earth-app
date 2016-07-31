using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class UnitConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Unit, Models.Unit>()
                .ForMember(opt => opt.Links, x => x.Ignore());

            // Precis mapping
            mapper.CreateMap<Unit, Models.Precis.UnitPrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}