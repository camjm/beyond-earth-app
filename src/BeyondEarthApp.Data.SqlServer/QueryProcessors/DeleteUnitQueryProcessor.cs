using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class DeleteUnitQueryProcessor : IDeleteUnitQueryProcessor
    {
        private readonly ISession _session;

        public DeleteUnitQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void DeleteUnit(long unitId)
        {
            var unit = _session.Get<Unit>(unitId);
            if (unit != null)
            {
                _session.Delete(unit);
            }
        }
    }
}
