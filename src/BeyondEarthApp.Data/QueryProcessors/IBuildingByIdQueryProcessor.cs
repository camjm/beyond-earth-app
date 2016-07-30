using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IBuildingByIdQueryProcessor
    {
        Building GetBuilding(long buildingId);
    }
}
