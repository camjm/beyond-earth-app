using System.Collections.Generic;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AllStatusesQueryProcessor : IAllStatusesQueryProcessor
    {
        private readonly ISession _session;

        public AllStatusesQueryProcessor(ISession session)
        {
            _session = session;
        }

        public IEnumerable<Status> GetStatuses()
        {
            var statuses = _session.QueryOver<Status>().List();
            return statuses;
        }
    }
}
