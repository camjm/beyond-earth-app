using BeyondEarthApp.Web.Api.Models.Precis;
using BeyondEarthApp.Web.Api.Models;
using BeyondEarthApp.Common;
using System.Net.Http;

namespace BeyondEarthApp.Web.Api.LinkServices.Precis
{
    public class GamePrecisLinkService : IGamePrecisLinkService
    {
        private const string PathFragmentBase = "games";

        private readonly ICommonLinkService _commonLinkService;

        public GamePrecisLinkService(ICommonLinkService commonLinkService)
        {
            _commonLinkService = commonLinkService;
        }

        public virtual void AddSelfLink(GamePrecis game)
        {
            game.AddLink(GetSelfLink(game.GameId));
        }

        public virtual Link GetSelfLink(long gameId)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, gameId);
            return _commonLinkService.GetLink(
                pathFragment,
                Constants.CommonLinkRelValues.Self,
                HttpMethod.Get);
        }

        public virtual Link GetAllGamesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}