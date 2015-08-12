using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class StatusConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Status, Models.Status>();
        }
    }
}