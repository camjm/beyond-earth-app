using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IUnitByIdQueryProcessor
    {
        Unit GetUnit(long unitId);
    }
}
