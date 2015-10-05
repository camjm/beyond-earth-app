using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models.Initial
{
    public class NewTechnology
    {
        public string Name { get; set; }

        public int Cost { get; set; }

        public List<Building> Buildings { get; set; }

        public List<Unit> Units { get; set; }
    }
}
