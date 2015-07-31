using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class NewGame
    {
        public string Description { get; set; }

        public List<Technology> Technologies { get; set; }

        public Faction Faction { get; set; }
    }
}
