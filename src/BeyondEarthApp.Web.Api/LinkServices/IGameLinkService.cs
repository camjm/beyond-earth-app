using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IGameLinkService
    {
        void AddLinks(Game game);

        void AddSelfLink(GamePrecis game);

        void AddLinksToChildren(Game game);

        Link GetSelfLink(long gameId);

        Link GetAllGamesLink();
    }
}
