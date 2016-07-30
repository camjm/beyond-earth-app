using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public interface IAddBuildingMaintenanceProcessor
    {
        Building AddBuilding(Building newBuilding);
    }
}
