using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.Entities;
using NHibernate;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.Exceptions;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AddUnitQueryProcessor : IAddUnitQueryProcessor
    {
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddUnitQueryProcessor(ISession session, IUserSession userSession)
        {
            _session = session;
            _userSession = userSession;
        }

        public void AddUnit(Unit unit)
        {
            if (unit.Technology != null)
            {
                var persistedTechnology = _session.Get<Technology>(unit.Technology.TechnologyId);
                if (persistedTechnology == null)
                {
                    throw new ChildObjectNotFoundException("Technology not found");
                }
                unit.Technology = persistedTechnology;
            }

            // Persist unit and its relationships
            _session.SaveOrUpdate(unit);
        }
    }
}
