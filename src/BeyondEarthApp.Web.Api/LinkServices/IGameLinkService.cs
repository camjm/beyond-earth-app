﻿using BeyondEarthApp.Web.Api.Models;

namespace BeyondEarthApp.Web.Api.LinkServices
{
    public interface IGameLinkService
    {
        void AddLinks(Game game);

        void AddSelfLink(Game game);

        void AddLinksToChildren(Game game);

        Link GetSelfLink(long gameId);

        Link GetAllGamesLink();
    }
}
