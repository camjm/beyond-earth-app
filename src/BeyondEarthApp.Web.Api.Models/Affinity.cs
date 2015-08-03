using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Affinity : ILinkContaining
    {
        public long AffinityId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<AffinityBonus> AffinityBonuses { get; set; }

        public List<TechnologyAffinity> AffinityTechnologies { get; set; }

        #region Links

        private List<Link> _links;

        public List<Link> Links
        {
            get { return _links ?? (_links = new List<Link>()); }
            set { _links = value; }
        }

        public void AddLink(Link link)
        {
            Links.Add(link);
        }

        #endregion
    }
}
