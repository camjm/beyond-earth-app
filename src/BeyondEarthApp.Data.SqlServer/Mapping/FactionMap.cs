using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class FactionMap : VersionedClassMap<Faction>
    {
        public FactionMap()
        {
            Id(x => x.FactionId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Leader).Not.Nullable();
            Map(x => x.Capital).Not.Nullable();
            Map(x => x.Ability).Not.Nullable();
        }
    }
}
