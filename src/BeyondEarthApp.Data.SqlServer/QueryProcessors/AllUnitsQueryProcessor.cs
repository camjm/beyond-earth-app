using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.Entities;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AllUnitsQueryProcessor : IAllUnitsQueryProcessor
    {
        private readonly ISession _session;

        public AllUnitsQueryProcessor(ISession session)
        {
            _session = session;
        }

        public QueryResult<Unit> GetUnits(PagedDataRequest requestInfo)
        {
            // IQueryOver instance provides queryable access to the table: haven't fetched any data from database yet.
            var query = _session.QueryOver<Unit>();

            // Queries the database
            var totalItemCount = query.ToRowCountQuery().RowCount();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            // Only 1 page of data is fetched from the database
            var units = query.Skip(startIndex).Take(requestInfo.PageSize).List();

            var queryResult = new QueryResult<Unit>(units, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}
