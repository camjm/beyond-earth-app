using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class TechnologyByIdQueryProcessor : ITechnologyByIdQueryProcessor
    {
        private readonly ISession _session;

        public TechnologyByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Technology GetTechnology(long technologyId)
        {
            var technology = _session.Get<Technology>(technologyId);
            return technology;
        }
    }
}
