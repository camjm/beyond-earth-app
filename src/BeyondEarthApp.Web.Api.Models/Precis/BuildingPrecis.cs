using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models.Precis
{
    public class BuildingPrecis
    {
        public long BuildingId { get; set; }

        public string Name { get; set; }

        public int Cost { get; set; }

        public string Technology { get; set; }

        #region Links

        private List<Link> _links;

        [Editable(false)]
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
