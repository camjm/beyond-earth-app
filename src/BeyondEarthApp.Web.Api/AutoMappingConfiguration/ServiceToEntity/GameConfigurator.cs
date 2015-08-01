using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class GameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Game, Data.Entities.Game>()
                .ForMember(opts => opts.User, x => x.Ignore())
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}