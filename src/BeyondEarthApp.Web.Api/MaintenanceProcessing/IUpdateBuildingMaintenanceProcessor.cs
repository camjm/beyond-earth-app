using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public interface IUpdateBuildingMaintenanceProcessor
    {
        Building UpdateBuilding(long buildingId, object buildingFragment);
    }
}
