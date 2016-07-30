using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IAllUnitsQueryProcessor
    {
        QueryResult<Unit> GetUnits(PagedDataRequest requestInfo);
    }
}
