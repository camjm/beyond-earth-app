using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Initial;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    /// <summary>
    /// Controller dependency to add a new game
    /// </summary>
    public interface IAddGameMaintenanceProcessor
    {
        Game AddGame(NewGame newGame);
    }
}
