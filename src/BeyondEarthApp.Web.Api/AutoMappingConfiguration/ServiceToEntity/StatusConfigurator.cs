using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class StatusConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Status, Data.Entities.Status>()
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}