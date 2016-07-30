using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class DeleteBuildingQueryProcessor : IDeleteBuildingQueryProcessor
    {
        private readonly ISession _session;

        public DeleteBuildingQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void DeleteBuilding(long buildingId)
        {
            var building = _session.Get<Building>(buildingId);
            if (building != null)
            {
                _session.Delete(building);
            }
        }
    }
}
