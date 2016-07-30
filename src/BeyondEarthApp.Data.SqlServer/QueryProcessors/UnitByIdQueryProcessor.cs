using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class UnitByIdQueryProcessor : IUnitByIdQueryProcessor
    {
        private readonly ISession _session;

        public UnitByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Unit GetUnit(long unitId)
        {
            var unit = _session.Get<Unit>(unitId);
            return unit;
        }
    }
}
