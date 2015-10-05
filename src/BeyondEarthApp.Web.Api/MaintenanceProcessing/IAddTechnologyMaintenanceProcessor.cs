using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;

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
