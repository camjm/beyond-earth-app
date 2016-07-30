using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class BuildingByIdQueryProcessor : IBuildingByIdQueryProcessor
    {
        private readonly ISession _session;

        public BuildingByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Building GetBuilding(long buildingId)
        {
            var building = _session.Get<Building>(buildingId);
            return building;
        }
    }
}
