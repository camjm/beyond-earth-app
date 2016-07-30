using BeyondEarthApp.Data.QueryProcessors;
using BeyondEarthApp.Data.Entities;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AllBuildingsQueryProcessor : IAllBuildingsQueryProcessor
    {
        private readonly ISession _session;

        public AllBuildingsQueryProcessor(ISession session)
        {
            _session = session;
        }

        public QueryResult<Building> GetBuildings(PagedDataRequest requestInfo)
        {
            // IQueryOver instance provides queryable access to the table: haven't fetched any data from database yet.
            var query = _session.QueryOver<Building>();

            // Queries the database
            var totalItemCount = query.ToRowCountQuery().RowCount();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            // Only 1 page of data is fetched from the database
            var buildings = query.Skip(startIndex).Take(requestInfo.PageSize).List();

            var queryResult = new QueryResult<Building>(buildings, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}
