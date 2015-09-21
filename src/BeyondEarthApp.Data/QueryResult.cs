using System.Collections.Generic;

namespace BeyondEarthApp.Data
{
    /// <summary>
    /// Paging-enhanced data transfer object (DTO). Encapsulates data returned by the query processor.
    /// </summary>
    public class QueryResult<T>
    {
        public QueryResult(IEnumerable<T> queriedItems, int totalItemCount, int pageSize)
        {
            QueriedItems = queriedItems;
            TotalItemCount = totalItemCount;
            PageSize = pageSize;
        }

        public IEnumerable<T> QueriedItems { get; private set; }

        public int TotalItemCount { get; private set; }

        public int PageSize { get; private set; }

        public int TotalPageCount
        {
            get { return ResultsPagingUtility.CalculatePageCount(TotalItemCount, PageSize); }
        }
    }
}
