using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public interface IGamePrecisLinkService
    {
        void AddSelfLink(GamePrecis game);

        Link GetAllGamesLink();
    }
}
