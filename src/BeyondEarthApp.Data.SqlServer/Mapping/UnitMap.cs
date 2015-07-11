using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class UnitMap : VersionedClassMap<Unit>
    {
        public UnitMap()
        {
            Id(x => x.UnitId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Cost).Not.Nullable();
            Map(x => x.Strength).Not.Nullable();

            References(x => x.Technology, "TechnologyId");
        }
    }
}
