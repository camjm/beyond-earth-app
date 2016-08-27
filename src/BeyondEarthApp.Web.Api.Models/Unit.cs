using BeyondEarthApp.Web.Api.Models.Precis;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeyondEarthApp.Web.Api.Models
{
    public class Unit : ILinkContaining
    {
        [Key]
        public long UnitId { get; set; }

        [Editable(true)]
        public string Name { get; set; }

        [Editable(true)]
        public int Cost { get; set; }

        [Editable(true)]
        public short Strength { get; set; }

        [Editable(true)]
        public TechnologyPrecis Technology { get; set; }

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
