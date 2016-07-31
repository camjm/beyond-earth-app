using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class GameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<Game, Data.Entities.Game>()
                .ForMember(opts => opts.User, x => x.Ignore())
                .ForMember(opt => opt.Version, x => x.Ignore());

            mapper.CreateMap<GamePrecis, Data.Entities.Game>()
                .ForMember(opts => opts.User, x => x.Ignore())
                .ForMember(opts => opts.CreatedDate, x => x.Ignore())
                .ForMember(opts => opts.CompletedDate, x => x.Ignore())
                .ForMember(opts => opts.Status, x => x.Ignore())
                .ForMember(opts => opts.Faction, x => x.Ignore())
                .ForMember(opts => opts.Technologies, x => x.Ignore())
                .ForMember(opt => opt.Version, x => x.Ignore());
        }
    }
}