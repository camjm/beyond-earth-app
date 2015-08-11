using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public interface IRestartGameProcessor
    {
        Game RestartGame(long gameId);
    }
}
