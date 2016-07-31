using BeyondEarthApp.Web.Api.Models.Precis;

namespace BeyondEarthApp.Web.Api.Models
{
    public class TechnologyAffinity
    {
        public TechnologyPrecis Technology { get; set; }

        public Affinity Affinity { get; set; }

        public int Amount { get; set; }
    }
}
