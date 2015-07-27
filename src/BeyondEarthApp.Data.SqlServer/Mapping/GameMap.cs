using BeyondEarthApp.Data.Entities;
using FluentNHibernate.Mapping;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class GameMap : VersionedClassMap<Game>
    {
        public GameMap()
        {
            Id(x => x.GameId);
            Map(x => x.Description).Nullable();
            Map(x => x.CreatedDate).Not.Nullable();

            References(x => x.User, "UserId");
            References(x => x.Faction, "FactionId");

            HasManyToMany(x => x.Technologies)
                .Access.ReadOnlyPropertyThroughCamelCaseField(Prefix.Underscore)
                .Table("GameTechnology")
                .ParentKeyColumn("GameId")
                .ChildKeyColumn("TechnologyId");
        }
    }
}
