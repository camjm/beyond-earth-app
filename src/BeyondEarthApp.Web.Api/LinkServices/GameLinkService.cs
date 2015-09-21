using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class GameLinkService : IGameLinkService
    {
        private const string PathFragmentBase = "games";

        private ICommonLinkService _commonLinkService;
        private IFactionLinkService _factionLinkService;
        private ITechnologyLinkService _technologyLinkService;

        public GameLinkService(
            ICommonLinkService commonLinkService, 
            IFactionLinkService factionLinkService, 
            ITechnologyLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _factionLinkService = factionLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddSelfLink(Game game)
        {
            game.AddLink(GetSelfLink(game));
        }

        public virtual Link GetSelfLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, game.GameId);

            var link = _commonLinkService.GetLink(
                pathFragment, 
                Constants.CommonLinkRelValues.Self, 
                HttpMethod.Get);

            return link;
        }

        public void AddLinksToChildren(Game game)
        {
            _factionLinkService.AddSelfLink(game.Faction);
            game.Technologies.ForEach(x => _technologyLinkService.AddSelfLink(x));
        }

        public Link GetAllGamesLink()
        {
            return _commonLinkService.GetLink(
                PathFragmentBase,
                Constants.CommonLinkRelValues.All,
                HttpMethod.Get);
        }
    }
}