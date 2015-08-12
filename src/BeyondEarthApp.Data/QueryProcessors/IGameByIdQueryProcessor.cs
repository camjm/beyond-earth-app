using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    /// <summary>
    /// Performs a simple 'Get' operation.
    /// Query Processors are part of a Strategy Pattern implementation to provide access to persistent data.
    /// </summary>
    public interface IGameByIdQueryProcessor
    {
        Game GetGame(long gameId);
    }
}
