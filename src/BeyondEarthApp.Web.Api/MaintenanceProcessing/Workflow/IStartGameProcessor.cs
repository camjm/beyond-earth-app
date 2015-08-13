using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    /// <summary>
    /// Encapsulates the business logic to start a game
    /// </summary>
    public interface IStartGameProcessor
    {
        Game StartGame(long gameId);
    }
}
