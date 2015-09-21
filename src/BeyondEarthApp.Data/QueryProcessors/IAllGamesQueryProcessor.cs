using BeyondEarthApp.Data.Entities;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IAllGamesQueryProcessor
    {
        QueryResult<Game> GetGames(PagedDataRequest requestInfo);
    }
}
