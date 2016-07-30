using BeyondEarthApp.Data.Entities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IUpdateBuildingQueryProcessor
    {
        Building GetUpdatedBuilding(long buildingId, PropertyValueMapType updatedPropertyValueMap);

        Building DeleteBuildingTechnology(long buildingId);

        Building ReplaceBuildingTechnology(long buildingId, long technologyId);
    }
}
