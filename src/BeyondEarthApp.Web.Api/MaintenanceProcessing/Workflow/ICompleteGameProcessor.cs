using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing.Workflow
{
    public interface ICompleteGameProcessor
    {
        Game CompleteGame(long gameId);
    }
}
