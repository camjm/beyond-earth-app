using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Web.Api.Models.Initial;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.ServiceToEntity
{
    public class NewGameConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<NewGame, Data.Entities.Game>()
                .ForMember(opt => opt.Version, x => x.Ignore())
                .ForMember(opt => opt.GameId, x => x.Ignore())
                .ForMember(opt => opt.Technologies, x => x.Ignore())
                .ForMember(opt => opt.User, x => x.Ignore())
                .ForMember(opt => opt.Status, x => x.Ignore())
                .ForMember(opt => opt.CreatedDate, x => x.Ignore())
                .ForMember(opt => opt.StartDate, x => x.Ignore())
                .ForMember(opt => opt.CompletedDate, x => x.Ignore());
        }
    }
}