using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// Controller dependency to add a new technology
    /// </summary>
    public interface IAddTechnologyMaintenanceProcessor
    {
        Technology AddTechnology(NewTechnology newTechnology);
    }
}
