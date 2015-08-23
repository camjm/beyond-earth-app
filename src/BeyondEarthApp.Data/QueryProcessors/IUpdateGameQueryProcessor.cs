using System.Collections.Generic;
using BeyondEarthApp.Data.Entities;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.QueryProcessors
{
    public interface IUpdateGameQueryProcessor
    {
        Game GetUpdatedGame(long gameId, PropertyValueMapType updatedPropertyValueMap);

        Game ReplaceGameTechnologies(long gameId, IEnumerable<long> technologyIds);

        Game DeleteGameTechnologies(long gameId);

        Game AddGameTechnology(long gameId, long technologyId);

        Game DeleteGameTechnology(long gameId, long technologyId);
    }
}
