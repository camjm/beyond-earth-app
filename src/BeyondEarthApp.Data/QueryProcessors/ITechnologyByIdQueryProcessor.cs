using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface ITechnologyByIdQueryProcessor
    {
        Technology GetTechnology(long technologyId);
    }
}
