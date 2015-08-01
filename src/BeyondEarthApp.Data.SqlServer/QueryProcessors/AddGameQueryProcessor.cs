using BeyondEarthApp.Common;
using BeyondEarthApp.Common.Security;
using BeyondEarthApp.Data.Entities;
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
            //game.User = _session.QueryOver<User>()
            //    .Where(x => x.UserName == _userSession.Username)
            //    .SingleOrDefault();
            game.User = _session.Get<User>(1L); // HACK

            game.Faction = _session.QueryOver<Faction>()
                .Where(x => x.FactionId == game.Faction.FactionId)
                .SingleOrDefault();

            // Persist game and its relationships
            _session.SaveOrUpdate(game);
        }
    }
}
