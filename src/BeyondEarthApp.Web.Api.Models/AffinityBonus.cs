using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class AffinityBonus : ILinkContaining
    {
        public long AffinityBonusId { get; set; }

        public Affinity Affinity { get; set; }

        public string Description { get; set; }

        public short Level { get; set; }

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
