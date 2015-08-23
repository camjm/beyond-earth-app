using System.Collections.Generic;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.MaintenanceProcessing
{
    public interface IGameTechnologiesMaintenceProcessor
    {
        Game ReplaceGameTechnologies(long gameId, IEnumerable<long> technologyIds);

        Game DeleteGameTechnologies(long gameId);

        Game AddGameTechnology(long gameId, long technologyId);

        Game DeleteGameTechnology(long gameId, long technologyId);
    }
}
