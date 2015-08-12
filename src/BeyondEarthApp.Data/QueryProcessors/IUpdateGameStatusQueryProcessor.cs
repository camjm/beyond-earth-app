using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IUpdateGameStatusQueryProcessor
    {
        void UpdateGameStatus(Game gameToUpdate, string statusName);
    }
}
