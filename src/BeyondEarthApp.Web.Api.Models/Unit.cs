using BeyondEarthApp.Web.Api.Models.Precis;
using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Unit : ILinkContaining
    {
        public long UnitId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public short Strength { get; set; }

        public TechnologyPrecis Technology { get; set; }

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
