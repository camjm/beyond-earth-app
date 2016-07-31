using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.Precis
{
    public class TechnologyConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Technology, Models.Precis.TechnologyPrecis>()
                .ForMember(x => x.Links, opts => opts.Ignore());
        }
    }
}