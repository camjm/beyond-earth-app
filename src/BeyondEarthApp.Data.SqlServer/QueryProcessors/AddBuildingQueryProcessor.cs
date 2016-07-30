using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.Entities;
using NHibernate;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.Exceptions;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AddBuildingQueryProcessor : IAddBuildingQueryProcessor
    {
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddBuildingQueryProcessor(ISession session, IUserSession userSession)
        {
            _session = session;
            _userSession = userSession;
        }

        public void AddBuilding(Building building)
        {
            if (building.Technology != null)
            {
                var persistedTechnology = _session.Get<Technology>(building.Technology.TechnologyId);
                if (persistedTechnology == null)
                {
                    throw new ChildObjectNotFoundException("Technology not found");
                }
                building.Technology = persistedTechnology;
            }

            // Persist building and its relationships
            _session.SaveOrUpdate(building);
        }
    }
}
