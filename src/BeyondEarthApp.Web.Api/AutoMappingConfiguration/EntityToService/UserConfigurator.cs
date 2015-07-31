using BeyondEarthApp.Common.TypeMapping;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Web.Api.AutoMappingConfiguration.Resolvers;

namespace BeyondEarthApp.Web.Api.AutoMappingConfiguration.EntityToService
{
    public class UserConfigurator : IAutoMapperTypeConfigurator
    {
        public void Configure(IAutoMapper mapper)
        {
            mapper.CreateMap<User, Models.User>()
                .ForMember(opt => opt.Links, x => x.Ignore())
                .ForMember(opt => opt.Games, x => x.ResolveUsing<UserGameResolver>());
        }
    }
}