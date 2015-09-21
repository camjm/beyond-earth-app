using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    /// <summary>
    /// Packages data from database into QueryResult.
    /// </summary>
    public class AllGamesQueryProcessor : IAllGamesQueryProcessor
    {
        private readonly ISession _session;

        public AllGamesQueryProcessor(ISession session)
        {
            _session = session;
        }

        public QueryResult<Game> GetGames(PagedDataRequest requestInfo)
        {
            // IQueryOver instance provides queryable access to the table: haven't fetched any data from database yet.
            var query = _session.QueryOver<Game>();

            // Queries the database
            var totalItemCount = query.ToRowCountQuery().RowCount();

            var startIndex = ResultsPagingUtility.CalculateStartIndex(requestInfo.PageNumber, requestInfo.PageSize);

            // Only 1 page of data is fetched from the database
            var games = query.Skip(startIndex).Take(requestInfo.PageSize).List();

            var queryResult = new QueryResult<Game>(games, totalItemCount, requestInfo.PageSize);

            return queryResult;
        }
    }
}
