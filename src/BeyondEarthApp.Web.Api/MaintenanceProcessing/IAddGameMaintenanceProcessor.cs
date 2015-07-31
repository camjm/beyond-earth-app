using BeyondEarthApp.Web.Api.Models;

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
