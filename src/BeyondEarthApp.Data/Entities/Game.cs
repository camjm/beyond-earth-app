using System;
using System.Collections.Generic;

namespace BeyondEarthApp.Data.Entities
{
    public class Game : IVersionedEntity
    {
        private readonly IList<Technology>  _technologies = new List<Technology>();

        public virtual long GameId { get; set; }

        public virtual string Description { get; set; }

        public virtual DateTime? StartDate { get; set; }

        public virtual Status Status { get; set; }

        public virtual User User { get; set; }

        public virtual Faction Faction { get; set; }

        public virtual DateTime CreatedDate { get; set; }

        public virtual IList<Technology> Technologies
        {
            get { return _technologies; }
        }

        public virtual byte[] Version { get; set; }
    }
}
