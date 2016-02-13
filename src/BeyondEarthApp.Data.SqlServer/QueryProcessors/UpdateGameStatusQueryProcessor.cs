using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class UpdateGameStatusQueryProcessor : IUpdateGameStatusQueryProcessor
    {
        private readonly ISession _session;

        public UpdateGameStatusQueryProcessor(ISession session)
        {
            _session = session;
        }

        public void UpdateGameStatus(Game gameToUpdate, string statusName)
        {
            var status = _session.QueryOver<Status>().Where(x => x.Name == statusName).SingleOrDefault();
            
            gameToUpdate.Status = status;

            _session.SaveOrUpdate(gameToUpdate);
        }
    }
}
