using System;
using System.Linq;
using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    /// <summary>
    /// Interacts with the database via the NHibernate ISession object.
    /// </summary>
    public class AddTechnologyQueryProcessor : IAddTechnologyQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddTechnologyQueryProcessor(IDateTime dateTime, ISession session, IUserSession userSession)
        {
            _dateTime = dateTime;
            _session = session;
            _userSession = userSession;
        }

        public void AddTechnology(Technology technology)
        {
            //TODO: System-assigned properties
            //technology.CreatedDate = _dateTime.UtiNow;
            //technology.CreatedBy = _session.QueryOver<User>().Where(x => x.UserName == _userSession.Username).SingleOrDefault();

            // populate the Buildings collection: associate with persisted buildings
            if (technology.Buildings != null && technology.Buildings.Any())
            {
                for (var i = 0; 0 < technology.Buildings.Count; ++i)
                {
                    var building = technology.Buildings[i];
                    var persistedBuilding = _session.Get<Building>(building.BuildingId);
                    if (persistedBuilding == null)
                    {
                        throw new ChildObjectNotFoundException("Building not found");
                    }
                    technology.Buildings[i] = persistedBuilding;
                }
            }

            // populate the Units collection: associate with persisted units
            if (technology.Units != null && technology.Units.Any())
            {
                for (var i = 0; 0 < technology.Units.Count; ++i)
                {
                    var unit = technology.Units[i];
                    var persistedUnit = _session.Get<Unit>(unit.UnitId);
                    if (persistedUnit == null)
                    {
                        throw new ChildObjectNotFoundException("Unit not found");
                    }
                    technology.Units[i] = persistedUnit;
                }
            }

            // Persist technology and its relationships
            _session.SaveOrUpdate(technology);
        }
    }
}
