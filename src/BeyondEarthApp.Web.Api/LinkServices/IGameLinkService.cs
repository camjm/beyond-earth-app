using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IGameLinkService
    {
        void AddSelfLink(Game game);

        void AddLinksToChildren(Game game);

        Link GetAllGamesLink();
    }
}
