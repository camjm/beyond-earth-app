using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class AddGameQueryProcessor : IAddGameQueryProcessor
    {
        private readonly IDateTime _dateTime;
        private readonly ISession _session;
        private readonly IUserSession _userSession;

        public AddGameQueryProcessor(IDateTime dateTime, ISession session, IUserSession userSession)
        {
            _dateTime = dateTime;
            _session = session;
            _userSession = userSession;
        }

        public void AddGame(Game game)
        {
            // System-assigned properties
            game.CreatedDate = _dateTime.UtcNow;
            game.Status = _session
                .QueryOver<Status>()
                .Where(x => x.Name == "Not Started")
                .SingleOrDefault();
            game.User = _session.QueryOver<User>()
                .Where(x => x.UserName == _userSession.Username)
                .SingleOrDefault();

            if (game.Faction != null)
            {
                var persistedFaction = _session.Get<Faction>(game.Faction.FactionId);
                if (persistedFaction == null)
                {
                    throw new ChildObjectNotFoundException("Faction not found");
                }
                game.Faction = persistedFaction;
            }

            // Persist game and its relationships
            _session.SaveOrUpdate(game);
        }
    }
}
