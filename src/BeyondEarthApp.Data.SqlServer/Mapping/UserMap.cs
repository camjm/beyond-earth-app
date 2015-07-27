using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class UserMap : VersionedClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.UserId);
            Map(x => x.FirstName).Not.Nullable();
            Map(x => x.LastName).Not.Nullable();
            Map(x => x.UserName).Not.Nullable();

            HasMany(x => x.Games).KeyColumn("UserId");
        }
    }
}
