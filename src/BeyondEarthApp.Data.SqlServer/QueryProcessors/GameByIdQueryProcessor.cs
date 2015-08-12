using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class GameByIdQueryProcessor : IGameByIdQueryProcessor
    {
        private readonly ISession _session;

        public GameByIdQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Game GetGame(long gameId)
        {
            var game = _session.Get<Game>(gameId);
            return game;
        }
    }
}
