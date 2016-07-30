using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public interface IAddUnitMaintenanceProcessor
    {
        Unit AddUnit(Unit newUnit);
    }
}
