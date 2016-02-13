using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class DeleteGameQueryProcessor : IDeleteGameQueryProcessor
    {
        private readonly ISession _session;

        public DeleteGameQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void DeleteGame(long gameId)
        {
            var game = _session.Get<Game>(gameId);
            if (game != null)
            {
                _session.Delete(game);
            }
        }
    }
}
