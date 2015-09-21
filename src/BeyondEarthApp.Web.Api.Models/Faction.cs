using System.Collections.Generic;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Faction : ILinkContaining
    {
        public long FactionId { get; set; }

        public string Name { get; set; }

        public string Leader { get; set; }

        public string Capital { get; set; }

        public string Ability { get; set; }

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
