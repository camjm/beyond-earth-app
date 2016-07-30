using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public interface IUpdateUnitMaintenanceProcessor
    {
        Unit UpdateUnit(long unitId, object unitFragment);
    }
}
