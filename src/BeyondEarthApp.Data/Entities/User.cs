using System.Collections.Generic;

namespace BeyondEarthApp.Data.Entities
{
    public class User : IVersionedEntity
    {
        private readonly IList<Game> _games = new List<Game>(); 

        public virtual long UserId { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string LastName { get; set; }

        public virtual string UserName { get; set; }

        public virtual IList<Game> Games
        {
            get { return _games; }
        }

        public virtual byte[] Version { get; set; }
    }
}
