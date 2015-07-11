using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.SqlServer.Mapping
{
    public class BuildingMap : VersionedClassMap<Building>
    {
        public BuildingMap()
        {
            Id(x => x.BuildingId);
            Map(x => x.Name).Not.Nullable();
            Map(x => x.Cost).Not.Nullable();

            References(x => x.Technology, "TechnologyId");
        }
    }
}
