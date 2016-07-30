using BeyondEarthApp.Data.Entities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IUpdateUnitQueryProcessor
    {
        Unit GetUpdatedUnit(long unitId, PropertyValueMapType updatedPropertyValueMap);

        Unit DeleteUnitTechnology(long unitId);

        Unit ReplaceUnitTechnology(long unitId, long technologyId);
    }
}
