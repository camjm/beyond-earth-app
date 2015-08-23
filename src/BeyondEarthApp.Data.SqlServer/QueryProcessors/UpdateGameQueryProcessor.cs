using BeyondEarthApp.Data.Entities;
using BeyondEarthApp.Data.Exceptions;
using BeyondEarthApp.Data.QueryProcessors;
using NHibernate;
using System.Collections.Generic;
using System.Linq;
using PropertyValueMapType = System.Collections.Generic.Dictionary<string, object>;

namespace BeyondEarthApp.Data.SqlServer.QueryProcessors
{
    public class UpdateGameQueryProcessor : IUpdateGameQueryProcessor
    {
        private readonly ISession _session;

        public UpdateGameQueryProcessor(ISession session)
        {
            _session = session;
        }

        public Game GetUpdatedGame(long gameId, PropertyValueMapType updatedPropertyValueMap)
        {
            var game = GetValidGame(gameId);

            var propertyInfos = typeof (Game).GetProperties();

            foreach (var propertyValuePair in updatedPropertyValueMap)
            {
                propertyInfos
                    .Single(x => x.Name == propertyValuePair.Key)
                    .SetValue(game, propertyValuePair.Value);
            }

            _session.SaveOrUpdate(game);

            return game;
        }

        public Game ReplaceGameTechnologies(long gameId, IEnumerable<long> technologyIds)
        {
            var game = GetValidGame(gameId);

            UpdateGameTechnologies(game, technologyIds, false);

            _session.SaveOrUpdate(game);    // Persist Game with updated Technologies associations

            return game;
        }

        public Game DeleteGameTechnologies(long gameId)
        {
            var game = GetValidGame(gameId);

            UpdateGameTechnologies(game, null, false);

            _session.SaveOrUpdate(game);    // Persist Game with updated Technologies associations

            return game;
        }

        public Game AddGameTechnology(long gameId, long technologyId)
        {
            var game = GetValidGame(gameId);

            UpdateGameTechnologies(game, new [] {technologyId}, true);

            _session.SaveOrUpdate(game);    // Persist Game with updated Technologies associations

            return game;
        }

        public Game DeleteGameTechnology(long gameId, long technologyId)
        {
            var game = GetValidGame(gameId);

            var technology = game.Technologies.FirstOrDefault(x => x.TechnologyId == technologyId);

            if (technology != null)
            {
                game.Technologies.Remove(technology);
                _session.SaveOrUpdate(game);    // Persist Game with updated Technologies associations
            }

            return game;
        }

        /// <summary>
        /// Fetches the Game from the database
        /// </summary>
        public virtual Game GetValidGame(long gameId)
        {
            var game = _session.Get<Game>(gameId);

            if (game == null)
            {
                throw new RootObjectNotFoundException("Game not found");
            }

            return game;
        }

        public virtual Technology GetValidTechnology(long technologyId)
        {
            var technology = _session.Get<Technology>(technologyId);

            if (technology == null)
            {
                throw new ChildObjectNotFoundException("Technology not found");
            }

            return technology;
        }

        /// <summary>
        /// Updates the Technologies Collection
        /// </summary>
        public virtual void UpdateGameTechnologies(Game game, IEnumerable<long> technologyIds, bool appendToExisting)
        {
            if (appendToExisting)
            {
                game.Technologies.Clear();
            }

            if (technologyIds != null)
            {
                foreach (var technology in technologyIds.Select(GetValidTechnology))
                {
                    if (!game.Technologies.Contains(technology))    // protects the idempotence of the operation
                    {
                        game.Technologies.Add(technology);
                    }
                }
            }
        }
    }
}
