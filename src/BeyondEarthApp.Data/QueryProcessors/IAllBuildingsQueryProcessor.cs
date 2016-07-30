using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IAllBuildingsQueryProcessor
    {
        QueryResult<Building> GetBuildings(PagedDataRequest requestInfo);
    }
}
