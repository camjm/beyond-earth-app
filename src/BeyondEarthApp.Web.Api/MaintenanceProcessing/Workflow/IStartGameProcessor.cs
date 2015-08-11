using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public interface IStartGameProcessor
    {
        Game StartGame(long gameId);
    }
}
