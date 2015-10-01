using System;
using System.Net.Http;
using BeyondEarthApp.Common;
using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public class GameLinkService : IGameLinkService
    {
        private const string PathFragmentBase = "games";

        private readonly ICommonLinkService _commonLinkService;
        private readonly IStatusLinkService _statusLinkService;
        private readonly IFactionLinkService _factionLinkService;
        private readonly ITechnologyLinkService _technologyLinkService;

        public GameLinkService(
            ICommonLinkService commonLinkService, 
            IStatusLinkService statusLinkService,
            IFactionLinkService factionLinkService, 
            ITechnologyLinkService technologyLinkService)
        {
            _commonLinkService = commonLinkService;
            _statusLinkService = statusLinkService;
            _factionLinkService = factionLinkService;
            _technologyLinkService = technologyLinkService;
        }

        public void AddLinks(Game game)
        {
            AddSelfLink(game);
            AddAllGamesLink(game);
            AddGameTechnologiesLink(game);
            AddAllStatusesLink(game);
            AddUpdateGameLink(game);
            AddCreateNewGameLink(game);
            AddDeleteTechnologyLink(game);
            AddAddTechnologyLink(game);
            AddDeleteTechnologiesLink(game);
            AddReplaceTechnologiesLink(game);
            AddWorkflowLink(game);
            AddLinksToChildren(game);
        }

        public virtual void AddSelfLink(Game game)
        {
            game.AddLink(GetSelfLink(game.GameId.Value));
        }

        public virtual void AddAllGamesLink(Game game)
        {
            game.AddLink(GetAllGamesLink());
        }

        public virtual void AddGameTechnologiesLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}/technologies", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "gameTechnologies", HttpMethod.Get);
            game.AddLink(link);
        }

        public virtual void AddAllStatusesLink(Game game)
        {
            game.AddLink(_statusLinkService.GetAllStatusesLink());
        }

        public virtual void AddUpdateGameLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "updateGame", HttpMethod.Put);
            game.AddLink(link);
        }

        public virtual void AddCreateNewGameLink(Game game)
        {
            var link = _commonLinkService.GetLink(PathFragmentBase, "createGame", HttpMethod.Post);
            game.AddLink(link);
        }

        public virtual void AddDeleteTechnologyLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}/technologies/technologyId", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "deleteTechnology", HttpMethod.Delete);
            game.AddLink(link);
        }

        public virtual void AddAddTechnologyLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}/technologies/technologyId", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "addTechnology", HttpMethod.Put);
            game.AddLink(link);
        }

        public virtual void AddDeleteTechnologiesLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}/technologies", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "deleteTechnologies", HttpMethod.Delete);
            game.AddLink(link);
        }

        public virtual void AddReplaceTechnologiesLink(Game game)
        {
            var pathFragment = string.Format("{0}/{1}/technologies", PathFragmentBase, game.GameId);
            var link = _commonLinkService.GetLink(pathFragment, "replaceTechnologies", HttpMethod.Put);
            game.AddLink(link);
        }

        public virtual void AddWorkflowLink(Game game)
        {
            const int notStarted = 1;
            const int inProgress = 2;
            const int completed = 3;

            string pathFragment;
            string relValue;

            switch (game.Status.StatusId)
            {
                case notStarted:
                    pathFragment = string.Format("{0}/{1}/activations", PathFragmentBase, game.GameId);
                    relValue = "activateGame";
                    break;
                case inProgress:
                    pathFragment = string.Format("{0}/{1}/completions", PathFragmentBase, game.GameId);
                    relValue = "completeGame";
                    break;
                case completed:
                    pathFragment = string.Format("{0}/{1}/reactivations", PathFragmentBase, game.GameId);
                    relValue = "re-activateGame";
                    break;
                default:
                    throw new InvalidOperationException("Invalid status: " + game.Status.StatusId);
            }

            var link = _commonLinkService.GetLink(pathFragment, relValue, HttpMethod.Put);
            game.AddLink(link);
        }

        public void AddLinksToChildren(Game game)
        {
            _factionLinkService.AddSelfLink(game.Faction);
            game.Technologies.ForEach(x => _technologyLinkService.AddSelfLink(x));
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